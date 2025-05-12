using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Users;
using RRHH.WebApi.Services;
using System.Linq; // Added for LINQ operations like .Select() on errors
using System.Threading.Tasks;

namespace RRHH.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            _logger.LogInformation("Registration attempt for username: {Username}", userRegisterDto.Username);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Registration failed for {Username} due to invalid model state.", userRegisterDto.Username);
                return BadRequest(ModelState);
            }

            var existingUser = await _userManager.FindByNameAsync(userRegisterDto.Username);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed: Username {Username} already exists.", userRegisterDto.Username);
                return BadRequest(new { Message = "Username already exists." });
            }

            // Consider adding a check for Id_Empleado existence if an EmpleadoRepository is available
            // e.g., var empleado = await _empleadoRepository.GetByIdAsync(userRegisterDto.Id_Empleado);
            // if (empleado == null) return BadRequest(new { Message = $"Empleado with ID {userRegisterDto.Id_Empleado} not found." });

            var newUser = new User
            {
                UserName = userRegisterDto.Username,
                Id_Empleado = userRegisterDto.Id_Empleado,
                Active = true // Default new users to active
            };

            var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Username} registered successfully.", userRegisterDto.Username);
                // Optionally assign a default role:
                // await _userManager.AddToRoleAsync(newUser, "User"); 

                // Consider returning a more specific DTO if needed, or just success
                return Ok(new 
                {
                    Message = "User registered successfully.", 
                    UserId = newUser.Id, 
                    Username = newUser.UserName 
                });
            }

            _logger.LogError("User registration failed for {Username}: {Errors}", 
                             userRegisterDto.Username, 
                             string.Join(", ", result.Errors.Select(e => e.Description)));

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            _logger.LogInformation("Login attempt for username: {Username}", userLoginDto.Username);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login failed for {Username} due to invalid model state.", userLoginDto.Username);
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user == null)
            {
                _logger.LogWarning("Login failed: User {Username} not found.", userLoginDto.Username);
                return Unauthorized(new { Message = "Invalid username or password." }); // Generic message for security
            }

            if (!user.Active) // Assuming your User model has an 'Active' property
            {
                _logger.LogWarning("Login failed: User {Username} is inactive.", userLoginDto.Username);
                return Unauthorized(new { Message = "Account is inactive." });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Username} logged in successfully.", userLoginDto.Username);
                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, roles);
                return Ok(new 
                {
                    Message = "Login successful.",
                    Token = token,
                    UserId = user.Id,
                    Username = user.UserName,
                    Roles = roles
                });
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Login failed: User {Username} account locked out.", userLoginDto.Username);
                return Unauthorized(new { Message = "Account locked out. Too many login attempts." });
            }
            
            if (result.IsNotAllowed)
            { 
                _logger.LogWarning("Login failed: User {Username} not allowed to sign in (e.g. email not confirmed if required).", userLoginDto.Username);
                // You might want to check for specific reasons like email confirmation if you have that enabled
                // For now, a generic message or one indicating the account isn't fully activated.
                return Unauthorized(new { Message = "Login not allowed. Please ensure your account is fully activated or contact support." });
            }

            _logger.LogWarning("Login failed for {Username}: Invalid password.", userLoginDto.Username);
            return Unauthorized(new { Message = "Invalid username or password." }); // Generic message for security
        }

        [HttpGet("diag/hashpassword/{password}")] // Temporary diagnostic endpoint
        public IActionResult HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password cannot be empty.");
            }

            // You can use _userManager if you have it, or directly PasswordHasher
            var hasher = new PasswordHasher<User>(); // Or inject IPasswordHasher<User>
            var HashedPassword = hasher.HashPassword(null, password); // User can be null for just hashing

            _logger.LogInformation("Diagnostic Hash for '{Password}': {HashedPassword}", password, HashedPassword);
            return Ok(new { PlainPassword = password, Hashed = HashedPassword });
        }
    }
}
