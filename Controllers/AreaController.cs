using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Area;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models.Dtos.Empresa;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de areas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
       
       private readonly IAreaRepository _repository;

       /// <summary>
       /// Constructor del controlador, que recibe una instancia de 
       /// la interfaz de la base de datos de Areas.
       /// </summary>
       public AreaController(IAreaRepository repository)
       {
            _repository = repository;
       }

       /// <summary>
       /// Obtiene todas las areas de la base de datos.
       /// </summary>
       /// <returns>Lista de areas.</returns>
       [HttpGet]
       public async Task<ActionResult<IEnumerable<Area>>> GetAll()
       {
            // Llamar a la interfaz de la base de datos para obtener todas las areas.
            var areas = await _repository.GetAllAsync();
            // Mapear las areas a DTO para enviar al cliente.
            var dtos = areas.Select(a => new AreaReadDto {
                ID = a.ID,
                Id_Empresa = a.Id_Empresa,
                Clave = a.Clave,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
       }

       /// <summary>
       /// Obtiene una area en particular por su id.
       /// </summary>
       /// <param name="id">Id de la area.</param>
       /// <returns>Area encontrada, o 404 si no existe.</returns>
       [HttpGet("{id}")]
        public async Task<ActionResult<AreaReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener la area por su id.
            var area = await _repository.GetByIdAsync(id);
            if (area == null)
            {
                // Si no se encuentra la area, devolver un 404.
                return NotFound();
            }
            // Mapear la area a DTO para enviar al cliente.
            var dto = new AreaReadDto {
                ID = area.ID,
                Id_Empresa = area.Id_Empresa,
                Clave = area.Clave,
                Nombre = area.Nombre,
                Descripcion = area.Descripcion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }

         /// <summary>
         /// Crea una nueva area en la base de datos.
         /// </summary>
         /// <param name="dto">Objeto con datos de la area.</param>
         /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
         [HttpPost]
        public async Task<ActionResult<AreaReadDto>> Create(AreaCreateDto dto)
        {
            // Crear una nueva area con los datos del dto.
            var area = new Area 
            {
                Id_Empresa = dto.Id_Empresa,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };
            // Agregar la area a la base de datos.
            await _repository.AddAsync(area);

            // Mapear la area a DTO para enviar al cliente.
            var readDto = new AreaReadDto
            {
                ID = area.ID,
                Id_Empresa = area.Id_Empresa,
                Clave = area.Clave,
                Nombre = area.Nombre,
                Descripcion = area.Descripcion
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new {id = area.ID}, readDto);
        }

         /// <summary>
         /// Actualiza una area existente en la base de datos.
         /// </summary>
         /// <param name="id">Id de la area a actualizar.</param>
         /// <param name="dto">Objeto con datos de la area.</param>
         /// <returns>204 No Content</returns>
         [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AreaUpdateDto dto)
        {
            // Buscar la area a actualizar por su id.
            var area = await _repository.GetByIdAsync(id);
            if (area == null) return NotFound();

            // Asignar solo los campos del dto.
            area.Clave = dto.Clave;
            area.Nombre = dto.Nombre;
            area.Descripcion = dto.Descripcion;

            // Actualizar la area en la base de datos.
            await _repository.UpdateAsync(area);
            // Enviar un 204 No Content.
            return NoContent();

        }

        /// <summary>
        /// Actualiza una area existente en la base de datos mediante un JSON Patch.
        /// </summary>
        /// <param name="id">Id de la area a actualizar.</param>
        /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar.</param>
        /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<AreaUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            // Buscar la area a actualizar por su id.
            var area = await _repository.GetByIdAsync(id);
            if (area == null) return NotFound();

            // Mapear entidad a DTO.
            var dto = new AreaUpdateDto
            {
                Clave = area.Clave,
                Nombre = area.Nombre,
                Descripcion = area.Descripcion
            };

            // Aplicar parche y validar modelo.
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)return BadRequest(ModelState);

            // Mapear de vuelta a la entidad. Lo hacemos asi
            // porque no podemos enviar directamente el objeto
            // AreaUpdateDto a la interfaz de la base de datos,
            // ya que esta ultima espera un objeto de tipo Area.
            area.Clave = dto.Clave;
            area.Nombre = dto.Nombre;
            area.Descripcion = dto.Descripcion;

            // Actualizar la area en la base de datos.
            await _repository.UpdateAsync(area);
            // Enviar un 204 No Content.
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