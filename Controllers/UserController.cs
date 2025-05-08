using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Users;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;   

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
            var users = await _repository.GetAllAsync();
            var dtos = users.Select(a => new UserReadDto {
                ID = a.ID,
                Id_Empleado = a.Id_Empleado,
                Username = a.Username,
                Password = a.Password,
                Active = a.Active
            });
            return Ok(dtos);
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
                ID = user.ID,
                Id_Empleado = user.Id_Empleado,
                Username = user.Username,
                Password = user.Password,
                Active = user.Active
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(UserCreateDto dto)
        {
            var user = new User
            {
                Id_Empleado = dto.Id_Empleado,
                Username = dto.Username,
                Password = dto.Password,
                Active = dto.Active
            };
            await _repository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.ID },
            new {user.ID, user.Username, user.Id_Empleado});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.Username = dto.Username;
            user.Password = dto.Password;
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
                Username = user.Username,
                Password = user.Password,
                Active = user.Active
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            user.Username = dto.Username;
            user.Password = dto.Password;
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