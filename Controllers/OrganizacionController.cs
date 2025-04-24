using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Organizacion;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;



namespace RRHH.WebApi.Controllers
{

    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de organizaciones.
    /// </summary>
    /// <remarks>
    /// El controlador se encarga de recibir las solicitudes HTTP
    /// y de llamar a la interfaz de la base de datos para obtener
    /// y manipular las organizaciones.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {

        // Instancia de OrganizacionRepository, para interactuar con la base de datos
        private readonly OrganizacionRepository _repository;

        /// <summary>
        /// Constructor de OrganizacionController.
        /// </summary>
        /// <param name="repository">Instancia de OrganizacionRepository.</param>
        /// El constructor se encarga de asignar la instancia de OrganizacionRepository,
        /// que es la clase que se encarga de interactuar con la base de datos para obtener
        /// y manipular las organizaciones.
        public OrganizacionController(OrganizacionRepository repository)
        {
            // Asigna la instancia de OrganizacionRepository
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las organizaciones de la base de datos.
        /// </summary>
        /// <returns>Lista de organizaciones.</returns>
        /// Esta funcion llama a GetAllAsync de la interfaz de la base de datos,
        /// que devuelve una lista de todas las organizaciones.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizacionReadDto>>> GetAll()
        {
            var orgs = await _repository.GetAllAsync(); // Llama a la implementacion de la interfaz de la base de datos
            var dtos = orgs.Select(o => new OrganizacionReadDto
            {
                // Crea un objeto Dto con los datos de la organizacion
                Id = o.Id,
                Clave = o.Clave,
                Nombre = o.Nombre,
                Fecha_Creacion = o.Fecha_Creacion
            });
            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene una organizacion en particular por su id.
        /// </summary>
        /// <param name="id">Id de la organizacion.</param>
        /// <returns>Organizacion encontrada, o 404 si no existe.</returns>
        /// Esta funcion llama a GetByIdSync de la interfaz de la base de datos,
        /// que devuelve una organizacion en particular por su id.
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizacionReadDto>> GetByID(int id)
        {
            var o = await _repository.GetByIdSync(id); // Llama a la implementacion de la interfaz de la base de datos
            if (o == null) return NotFound();
            var dto = new OrganizacionReadDto
            {
                // Crea un objeto Dto con los datos de la organizacion
                Id = o.Id,
                Clave = o.Clave,
                Nombre = o.Nombre,
                Fecha_Creacion = o.Fecha_Creacion
            };
            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva organizacion.
        /// </summary>
        /// <param name="organizacion">Objeto con datos de la organizacion.</param>
        /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
        /// Esta funcion llama a AddAsync de la interfaz de la base de datos,
        /// que se encarga de agregar una nueva organizacion a la base de datos.
        [HttpPost]
        public async Task<ActionResult<OrganizacionReadDto>> Create(OrganizacionCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var o = new Organizacion
            {
                Clave = dto.Clave,
                Nombre = dto.Nombre,
                Fecha_Creacion = dto.Fecha_Creacion
            };
            await _repository.AddAsync(o);
            var readDto = new OrganizacionReadDto
            {
                Id = o.Id,
                Clave = o.Clave,
                Nombre = o.Nombre,
                Fecha_Creacion = o.Fecha_Creacion
            };
            return CreatedAtAction(nameof(GetByID), new { id = o.Id }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrganizacionUpdateDto dto)
        {
            var o = await _repository.GetByIdSync(id);
            if (o == null) return NotFound();

            o.Clave = dto.Clave;
            o.Nombre = dto.Nombre;
            o.Fecha_Creacion = dto.Fecha_Creacion;

            await _repository.UpdateAsync(o);
            return NoContent();
        }

        /// <summary>
        /// Actualiza una organizacion existente en la base de datos mediante un JSON Patch
        /// </summary>
        /// <param name="id">Id de la organizacion a actualizar</param>
        /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar</param>
        /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<OrganizacionUpdateDto> patchDoc)
        {
            // Verifica que el patchDoc no sea nulo
            if (patchDoc == null)
                return BadRequest();

            // Busca la organizacion por su id
            var organizacion = await _repository.GetByIdSync(id);

            // Si la organizacion no existe, regresa 404 Not Found
            if (organizacion == null)
                return NotFound();

            // Crea un objeto DTO con los datos de la organizacion encontrada
            var dto = new OrganizacionUpdateDto
            {
                Clave = organizacion.Clave,
                Nombre = organizacion.Nombre,
                Fecha_Creacion = organizacion.Fecha_Creacion
            };

            // Aplica el patch al objeto DTO y valida el modelo
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Asigna los valores actualizados del DTO a la organizacion encontrada
            organizacion.Clave = dto.Clave;
            organizacion.Nombre = dto.Nombre;
            organizacion.Fecha_Creacion = dto.Fecha_Creacion;

            // Actualiza la organizacion en la base de datos
            await _repository.UpdateAsync(organizacion);

            // Regresa 204 No Content
            return NoContent();
        }
        /// <summary>
        /// Elimina una organizacion por su id
        /// </summary>
        /// <param name="id">Id de la organizacion a eliminar</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca la organizacion por su id
            await _repository.DeleteAsync(id);
            // Si se encuentra y se elimina, devuelve 204 No Content
            return NoContent();
        }

    }
}