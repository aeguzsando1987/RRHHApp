using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.PuestosDescriptivo;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosDescriptivoController : ControllerBase
    {
        private readonly PuestosDescriptivoRepository _repository;

        public PuestosDescriptivoController(PuestosDescriptivoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los puestos descriptivos de la base de datos.
        /// </summary>
        /// <returns>Lista de puestos descriptivos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuestosDescriptivoReadDto>>> GetAll()
        {
            // Llamar a la interfaz de la base de datos para obtener todas las puestos descriptivos.
            var psd = await _repository.GetAllAsync();
            // Mapear las puestos descriptivos a DTO para enviar al cliente.
            var dtos = psd.Select(a => new PuestosDescriptivoReadDto {
                ID = a.ID,
                ID_Puesto = a.ID_Puesto,
                Resumen = a.Resumen,
                Fecha_Actualizacion = a.Fecha_Actualizacion
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PuestosDescriptivoReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener la puestos descriptivos por su id.
            var psd = await _repository.GetByIdAsync(id);
            if (psd == null)
            {
                // Si no se encuentra la puestos descriptivos, devolver un 404.
                return NotFound();
            }
            // Mapear la puestos descriptivos a DTO para enviar al cliente.
            var dto = new PuestosDescriptivoReadDto {
                ID = psd.ID,
                ID_Puesto = psd.ID_Puesto,
                Resumen = psd.Resumen,
                Fecha_Actualizacion = psd.Fecha_Actualizacion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }


        [HttpPost]
        public async Task<ActionResult<PuestosDescriptivoReadDto>> Create(PuestosDescriptivoCreateDto dto)
        {
            // Crear una nueva puestos descriptivos con los datos del dto.
            var psd = new PuestosDescriptivo 
            {
                ID_Puesto = dto.ID_Puesto,
                Resumen = dto.Resumen,
                Fecha_Actualizacion = dto.Fecha_Actualizacion
            };
            // Agregar la puestos descriptivos a la base de datos.
            await _repository.AddAsync(psd);

            // Mapear la puestos descriptivos a DTO para enviar al cliente.
            var readDto = new PuestosDescriptivoReadDto 
            {
                ID = psd.ID,
                ID_Puesto = psd.ID_Puesto,
                Resumen = psd.Resumen,
                Fecha_Actualizacion = psd.Fecha_Actualizacion
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new {id = psd.ID}, readDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PuestosDescriptivoUpdateDto dto)
        {
            // Buscar  puestos descriptivos a actualizar por su id.
            var psd = await _repository.GetByIdAsync(id);
            if (psd == null) return NotFound();

            // Asignar solo los campos del dto.
            psd.ID_Puesto = dto.ID_Puesto;
            psd.Resumen = dto.Resumen;
            psd.Fecha_Actualizacion = dto.Fecha_Actualizacion;

            // Actualizar  puestos descriptivos en la base de datos.
            await _repository.UpdateAsync(psd);
            // Enviar un 204 No Content.
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PuestosDescriptivoUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            // Buscar puestos descriptivos a actualizar por su id.
            var psd = await _repository.GetByIdAsync(id);
            if (psd == null) return NotFound();

            // Mapear entidad a DTO.
            var dto = new PuestosDescriptivoUpdateDto
            {
                ID_Puesto = psd.ID_Puesto,
                Resumen = psd.Resumen,
                Fecha_Actualizacion = psd.Fecha_Actualizacion
            };

            // Aplicar parche y validar modelo.
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)return BadRequest(ModelState);

            // Mapear de vuelta a la entidad. Lo hacemos asi
            // porque no podemos enviar directamente el objeto
            // PuestoUpdateDto a la interfaz de la base de datos,
            // ya que esta ultima espera un objeto de tipo Puesto.
            psd.ID_Puesto = dto.ID_Puesto;
            psd.Resumen = dto.Resumen;
            psd.Fecha_Actualizacion = dto.Fecha_Actualizacion;
            // Actualizar puestos descriptivos en la base de datos.
            await _repository.UpdateAsync(psd);
            // Enviar un 204 No Content.
            return NoContent();
        }


    }

}