using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models.Dtos.Puesto;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de puestos.
    /// </summary>
    // La ruta base de este controlador es "api/Puesto".
    // Todas las rutas definidas en este controlador seran "api/Puesto/<ruta-definida>".
    [Route("api/[controller]")]
    // Este controlador es un controlador de API, es decir que se encarga de recibir 
    // solicitudes HTTP y de responder en formato JSON.
    [ApiController]
    public class PuestoController : ControllerBase
    {
       
       private readonly PuestoRepository _repository;

       /// <summary>
       /// Constructor del controlador, que recibe una instancia de 
       /// la interfaz de la base de datos de Puestos.
       /// </summary>
       public PuestoController(PuestoRepository repository)
       {
            _repository = repository;
       }
       
       /// <summary>
       /// Obtiene todas los puestos de la base de datos.
       /// </summary>
       /// <returns>Lista de puestos.</returns>
       [HttpGet]
       public async Task<ActionResult<IEnumerable<Puesto>>> GetAll()
       {
            // Llamar a la interfaz de la base de datos para obtener todas los puestos.
            var puestos = await _repository.GetAllAsync();
            // Mapear los puestos a DTO para enviar al cliente.
            var dtos = puestos.Select(a => new PuestoReadDto {
                ID = a.ID,
                Clave = a.Clave,
                Id_Departamento = a.Id_Departamento,
                Id_Jerarquia = a.Id_Jerarquia,
                Titulo = a.Titulo,
                Descripcion = a.Descripcion
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
       }

       /// <summary>
       /// Obtiene un puesto en particular por su id.
       /// </summary>
       /// <param name="id">Id del puesto.</param>
       /// <returns>Puesto encontrada, o 404 si no existe.</returns>
       [HttpGet("{id}")]
        public async Task<ActionResult<PuestoReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener el puesto por su id.
            var puesto = await _repository.GetByIdAsync(id);
            if (puesto == null)
            {
                // Si no se encuentra el puesto, devolver un 404.
                return NotFound();
            }
            // Mapear el puesto a DTO para enviar al cliente.
            var dto = new PuestoReadDto {
                ID = puesto.ID,
                Clave = puesto.Clave,
                Id_Departamento = puesto.Id_Departamento,
                Id_Jerarquia = puesto.Id_Jerarquia,
                Titulo = puesto.Titulo,
                Descripcion = puesto.Descripcion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }
            

         /// <summary>
         /// Crea un nuevo puesto en la base de datos.
         /// </summary>
         /// <param name="dto">Objeto con datos del puesto.</param>
         /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
         [HttpPost]
        public async Task<ActionResult<PuestoReadDto>> Create(PuestoCreateDto dto)
        {
            // Crear un nuevo puesto con los datos del dto.
            var puesto = new Puesto 
            {
                Id_Departamento = dto.Id_Departamento,
                Id_Jerarquia = dto.Id_Jerarquia,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion
            };
            // Agregar el puesto a la base de datos.
            await _repository.AddSync(puesto);

            // Mapear el puesto a DTO para enviar al cliente.
            var readDto = new PuestoReadDto
            {
                ID = puesto.ID,
                Id_Departamento = puesto.Id_Departamento,
                Id_Jerarquia = puesto.Id_Jerarquia,
                Titulo = puesto.Titulo,
                Descripcion = puesto.Descripcion
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new {id = puesto.ID}, readDto);
        }

         /// <summary>
         /// Actualiza un puesto existente en la base de datos.
         /// </summary>
         /// <param name="id">Id del puesto a actualizar.</param>
         /// <param name="dto">Objeto con datos del puesto.</param>
         /// <returns>204 No Content</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PuestoUpdateDto dto)
        {
            // Buscar el puesto a actualizar por su id.
            var puesto = await _repository.GetByIdAsync(id);
            if (puesto == null) return NotFound();

            // Asignar solo los campos del dto.
            puesto.Clave = dto.Clave;
            puesto.Titulo = dto.Titulo;
            puesto.Descripcion = dto.Descripcion;

            // Actualizar el puesto en la base de datos.
            await _repository.UpdateAsync(puesto);
            // Enviar un 204 No Content.
            return NoContent();

        }
            // Enviar un 204 No Content.
 

       /// <summary>
       /// Actualiza un puesto existente en la base de datos mediante un JSON Patch.
       /// </summary>
       /// <param name="id">Id del puesto a actualizar. Tipo int.</param>
       /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar. Tipo JsonPatchDocument<PuestoUpdateDto>.</param>
       /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<PuestoUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            // Buscar el puesto a actualizar por su id.
            var puesto = await _repository.GetByIdAsync(id);
            if (puesto == null) return NotFound();

            // Mapear entidad a DTO.
            var dto = new PuestoUpdateDto
            {
                Clave = puesto.Clave,
                Titulo = puesto.Titulo,
                Descripcion = puesto.Descripcion
            };

            // Aplicar parche y validar modelo.
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)return BadRequest(ModelState);

            // Mapear de vuelta a la entidad. Lo hacemos asi
            // porque no podemos enviar directamente el objeto
            // PuestoUpdateDto a la interfaz de la base de datos,
            // ya que esta ultima espera un objeto de tipo Puesto.
            puesto.Clave = dto.Clave;
            puesto.Titulo = dto.Titulo;
            puesto.Descripcion = dto.Descripcion;

            // Actualizar el puesto en la base de datos.
            await _repository.UpdateAsync(puesto);
            // Enviar un 204 No Content.
            return NoContent();
        }

        /// <summary>
        /// Elimina un puesto existente en la base de datos.
        /// </summary>
        /// <param name="id">Id del puesto a eliminar. Tipo int.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}