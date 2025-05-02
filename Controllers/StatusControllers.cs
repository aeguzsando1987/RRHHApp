using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Status;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System.Runtime.CompilerServices;

namespace RRHH.WebApi.Controllers
{


    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de estados de personal.
    /// </summary>
    [Route("api/status")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusRepository _repository;

        /// <summary>
        /// Constructor del controlador.
        /// </summary>
        /// <param name="repository">Interfaz de la base de datos.</param>
        public StatusController(StatusRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los estados de personal.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista de DTOs de lectura de estados de personal.
        /// </remarks>
        /// <returns>Lista de DTOs de lectura de estados de personal.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusReadDto>>> GetAll()
        {
            var statuses = await _repository.GetAllAsync();
            var dtos = statuses.Select(s => new StatusReadDto
            {
                ID = s.ID,
                Status_Emp = s.Status_Emp,
                Descripcion_Status = s.Descripcion_Status        
            });
            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene un estado de personal en particular por su id.
        /// </summary>
        /// <param name="id">Id del estado de personal a obtener.</param>
        /// <returns>DTO de lectura del estado de personal o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusReadDto>> GetByID(int id)
        {
            var s = await _repository.GetByIdAsync(id);
            if(s == null) return NotFound();
            var dto = new StatusReadDto
            {
                ID = s.ID,
                Status_Emp = s.Status_Emp,
                Descripcion_Status = s.Descripcion_Status
            };
            return Ok(dto);
        }

        /// <summary>
        /// Crea un nuevo estado de personal.
        /// </summary>
        /// <param name="dto">DTO con datos del estado de personal a crear.</param>
        /// <returns>DTO de lectura del estado de personal creado o 400 si no es posible crearlo.</returns>
        [HttpPost]
        public async Task<ActionResult<StatusReadDto>> Create(StatusCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var s = new Status
            {
                Status_Emp = dto.Status_Emp,
                Descripcion_Status = dto.Descripcion_Status
            };

            await _repository.AddAsync(s);
            var readDto = new StatusReadDto
            {
                ID = s.ID,
                Status_Emp = s.Status_Emp,
                Descripcion_Status = s.Descripcion_Status
            };
            return CreatedAtAction(nameof(GetByID), new {id = s.ID}, readDto);
        }

        /// <summary>
        /// Actualiza un estado de personal existente en la base de datos.
        /// </summary>
        /// <param name="id">Id del estado de personal a actualizar.</param>
        /// <param name="dto">DTO con datos del estado de personal a actualizar.</param>
        /// <returns>204 No Content</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StatusUpdateDto dto)
        {
            var s = await _repository.GetByIdAsync(id);
            if (s == null) return NotFound();

            s.Status_Emp = dto.Status_Emp;
            s.Descripcion_Status = dto.Descripcion_Status;

            await _repository.UpdateAsync(s);
            return NoContent();
        }

        /// <summary>
        /// Actualiza un estado de personal existente en la base de datos mediante un JSON Patch.
        /// </summary>
        /// <param name="id">Id del estado de personal a actualizar.</param>
        /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar.</param>
        /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<StatusUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var s = await _repository.GetByIdAsync(id);

            if (s == null)
                return NotFound();

            var dto = new StatusUpdateDto
            {
                Status_Emp = s.Status_Emp,
                Descripcion_Status = s.Descripcion_Status
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            s.Status_Emp = dto.Status_Emp;
            s.Descripcion_Status = dto.Descripcion_Status;

            await _repository.UpdateAsync(s);
            return NoContent();
        }

        /// <summary>
        /// Elimina un estado de personal por su id.
        /// </summary>
        /// <param name="id">Id del estado de personal a eliminar.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }

}