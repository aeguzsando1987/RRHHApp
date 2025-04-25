using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models.Dtos.Departamento;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de departamentos.
    /// </summary>
    // La ruta base de este controlador es "api/Departamento".
    // Todas las rutas definidas en este controlador seran "api/Departamento/<ruta-definida>".
    [Route("api/[controller]")]
    // Este controlador es un controlador de API, es decir que se encarga de recibir 
    // solicitudes HTTP y de responder en formato JSON.
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
       
       private readonly DepartamentoRepository _repository;

       /// <summary>
       /// Constructor del controlador, que recibe una instancia de 
       /// la interfaz de la base de datos de Areas.
       /// </summary>
       public DepartamentoController(DepartamentoRepository repository)
       {
            _repository = repository;
       }

       /// <summary>
       /// Obtiene todas las departamentos de la base de datos.
       /// </summary>
       /// <returns>Lista de departamentos.</returns>
       [HttpGet]
       public async Task<ActionResult<IEnumerable<Departamento>>> GetAll()
       {
            // Llamar a la interfaz de la base de datos para obtener todas las departamentos.
            var departamamentos = await _repository.GetAllAsync();
            // Mapear las departamentos a DTO para enviar al cliente.
            var dtos = departamamentos.Select(a => new DepartamentoReadDto {
                ID = a.ID,
                Id_Area = a.Id_Area,
                Clave = a.Clave,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
       }

       /// <summary>
       /// Obtiene una departamento en particular por su id.
       /// </summary>
       /// <param name="id">Id de la departamento.</param>
       /// <returns>Departamento encontrada, o 404 si no existe.</returns>
       [HttpGet("{id}")]
        public async Task<ActionResult<DepartamentoReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener la departamento por su id.
            var departamento = await _repository.GetByIdAsync(id);
            if (departamento == null)
            {
                // Si no se encuentra la departamento, devolver un 404.
                return NotFound();
            }
            // Mapear la departamento a DTO para enviar al cliente.
            var dto = new DepartamentoReadDto {
                ID = departamento.ID,
                Id_Area = departamento.Id_Area,
                Clave = departamento.Clave,
                Nombre = departamento.Nombre,
                Descripcion = departamento.Descripcion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }

         /// <summary>
         /// Crea una nueva departamento en la base de datos.
         /// </summary>
         /// <param name="dto">Objeto con datos de la departamento.</param>
         /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
         [HttpPost]
        public async Task<ActionResult<DepartamentoReadDto>> Create(DepartamentoCreateDto dto)
        {
            // Crear una nueva departamento con los datos del dto.
            var departamento = new Departamento 
            {
                Id_Area = dto.Id_Area,
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };
            // Agregar la departamento a la base de datos.
            await _repository.AddSync(departamento);

            // Mapear la departamento a DTO para enviar al cliente.
            var readDto = new DepartamentoReadDto
            {
                ID = departamento.ID,
                Id_Area = departamento.Id_Area,
                Clave = departamento.Clave,
                Nombre = departamento.Nombre,
                Descripcion = departamento.Descripcion
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new {id = departamento.ID}, readDto);
        }

         /// <summary>
         /// Actualiza una departamento existente en la base de datos.
         /// </summary>
         /// <param name="id">Id de la departamento a actualizar.</param>
         /// <param name="dto">Objeto con datos de la departamento.</param>
         /// <returns>204 No Content</returns>
         [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartamentoUpdateDto dto)
        {
            // Buscar la departamento a actualizar por su id.
            var departamento = await _repository.GetByIdAsync(id);
            if (departamento == null) return NotFound();

            // Asignar solo los campos del dto.
            departamento.Clave = dto.Clave;
            departamento.Nombre = dto.Nombre;
            departamento.Descripcion = dto.Descripcion;

            // Actualizar la departamento en la base de datos.
            await _repository.UpdateAsync(departamento);
            // Enviar un 204 No Content.
            return NoContent();

        }

       /// <summary>
       /// Actualiza una departamento existente en la base de datos mediante un JSON Patch.
       /// </summary>
       /// <param name="id">Id de la departamento a actualizar. Tipo int.</param>
       /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar. Tipo JsonPatchDocument<DepartamentoUpdateDto>.</param>
       /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<DepartamentoUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            // Buscar la departamento a actualizar por su id.
            var departamento = await _repository.GetByIdAsync(id);
            if (departamento == null) return NotFound();

            // Mapear entidad a DTO.
            var dto = new DepartamentoUpdateDto
            {
                Clave = departamento.Clave,
                Nombre = departamento.Nombre,
                Descripcion = departamento.Descripcion
            };

            // Aplicar parche y validar modelo.
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)return BadRequest(ModelState);

            // Mapear de vuelta a la entidad. Lo hacemos asi
            // porque no podemos enviar directamente el objeto
            // DepartamentoUpdateDto a la interfaz de la base de datos,
            // ya que esta ultima espera un objeto de tipo Departamento.
            departamento.Clave = dto.Clave;
            departamento.Nombre = dto.Nombre;
            departamento.Descripcion = dto.Descripcion;

            // Actualizar la departamento en la base de datos.
            await _repository.UpdateAsync(departamento);
            // Enviar un 204 No Content.
            return NoContent();
        }

        /// <summary>
        /// Elimina una departamento existente en la base de datos.
        /// </summary>
        /// <param name="id">Id de la departamento a eliminar. Tipo int.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}