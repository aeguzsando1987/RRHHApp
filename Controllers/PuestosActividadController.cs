using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.PuestosActividad;
using RRHH.WebApi.Repositories;
namespace RRHH.WebApi.Controllers
{


        /// <summary>
        /// Controlador para la creacion, edicion y eliminacion de actividades de puestos descriptivos.
        /// </summary>
        /// <remarks>
        /// La ruta base de este controlador es "api/PuestosActividad".
        /// Todas las rutas definidas en este controlador seran "api/PuestosActividad&quot;.
        /// </remarks>
    [Route("api/controller")]
    [ApiController]
    public class PuestosActividadController : ControllerBase
    {

        private readonly PuestosActividadRepository _repository;

        public PuestosActividadController(PuestosActividadRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuestosActividadReadDto>>> GetAll()
        {
            var psa = await _repository.GetAllAsync();
            var dtos = psa.Select(a => new PuestosActividadReadDto{
                ID = a.ID,
                ID_PuestosDescriptivo = a.ID_PuestosDescriptivo,
                Titulo = a.Titulo,
                Resumen = a.Resumen,
                Fecha_Actualizacion = a.Fecha_Actualizacion
            });

            return Ok(dtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PuestosActividadReadDto>> GetById(int id)
        {
            var psa = await _repository.GetByIDAsync(id);
            if(psa == null)
            {
                return NotFound();
            }

            var dto = new PuestosActividadReadDto
            {
                ID = psa.ID,
                ID_PuestosDescriptivo = psa.ID_PuestosDescriptivo,
                Titulo = psa.Titulo,
                Resumen = psa.Resumen,
                Fecha_Actualizacion = psa.Fecha_Actualizacion
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PuestosActividadReadDto>> Create(PuestosActividadCreateDto dto)
        {
            var psa = new PuestosActividad
            {
                ID_PuestosDescriptivo = dto.ID_PuestosDescriptivo,
                Titulo = dto.Titulo,
                Resumen = dto.Resumen,
                Fecha_Actualizacion = dto.Fecha_Actualizacion
            };
            await _repository.AddAsync(psa);

            var readDto = new PuestosActividadReadDto
            {
                ID = psa.ID,
                ID_PuestosDescriptivo = psa.ID_PuestosDescriptivo,
                Titulo = psa.Titulo,
                Resumen = psa.Resumen,
                Fecha_Actualizacion = psa.Fecha_Actualizacion
            };
            return CreatedAtAction(nameof(GetById), new {id = psa.ID}, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PuestosActividadUpdateDto dto)
        {
            var psa = await _repository.GetByIDAsync(id);
            if(psa == null)
            {
                return NotFound();
            }

            psa.ID_PuestosDescriptivo = dto.ID_PuestosDescriptivo;
            psa.Titulo = dto.Titulo;
            psa.Resumen = dto.Resumen;
            psa.Fecha_Actualizacion = dto.Fecha_Actualizacion;

            await _repository.UpdateAsync(psa);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PuestosActividadUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var psa = await _repository.GetByIDAsync(id);
            if (psa == null) return NotFound();

            var dto = new PuestosActividadUpdateDto
            {
                ID_PuestosDescriptivo = psa.ID_PuestosDescriptivo,
                Titulo = psa.Titulo,
                Resumen = psa.Resumen,
                Fecha_Actualizacion = psa.Fecha_Actualizacion
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)return BadRequest(ModelState);

            psa.ID_PuestosDescriptivo = dto.ID_PuestosDescriptivo;
            psa.Titulo = dto.Titulo;
            psa.Resumen = dto.Resumen;
            psa.Fecha_Actualizacion = dto.Fecha_Actualizacion;

            await _repository.UpdateAsync(psa);
            return NoContent();
        }

        /// <summary>
/// Deletes an existing entry from the repository.
/// </summary>
/// <param name="id">The identifier of the entry to be deleted.</param>
/// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }


}