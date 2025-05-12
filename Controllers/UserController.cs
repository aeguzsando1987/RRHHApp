using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Users;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;   
using Microsoft.AspNetCore.Http;

namespace RRHH.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            // var users = await _repository.GetAllAsync();
            // var dtos = users.Select(a => new UserReadDto {
            //     ID = a.Id,
            //     Id_Empleado = a.Id_Empleado,
            //     Username = a.UserName,
            //     Active = a.Active
            // });
            // return Ok(dtos);
            return StatusCode(StatusCodes.Status501NotImplemented, new { Message = "This endpoint is deprecated or currently not available." });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var dto = new UserReadDto {
                ID = user.Id,
                Id_Empleado = user.Id_Empleado,
                Username = user.UserName,
                Active = user.Active
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(UserCreateDto dto)
        {
            // var user = new User
            // {
            //     Id_Empleado = dto.Id_Empleado,
            //     UserName = dto.Username,
            //     Active = dto.Active
            // };
            // await _repository.AddAsync(user); 
            // return CreatedAtAction(nameof(GetById), new { id = user.Id },
            // new {ID = user.Id, Username = user.UserName, user.Id_Empleado});
            
            // Deprecate this endpoint. User creation should go through AuthController.
            // Consider logging this attempt if monitoring is important.
            // _logger.LogWarning("Attempt to use deprecated UserController.Create endpoint for Username: {Username}", dto.Username);
            return StatusCode(StatusCodes.Status501NotImplemented, new { Message = "This endpoint is deprecated for user creation. Please use /api/auth/register." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = dto.Username;
            user.Active = dto.Active;

            await _repository.UpdateAsync(user);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound();

            var dto = new UserUpdateDto
            {
                Username = user.UserName,
                Active = user.Active
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.UserName = dto.Username;
            user.Active = dto.Active;

            await _repository.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}