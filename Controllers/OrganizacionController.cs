using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Controllers
{

    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de organizaciones.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacionController : ControllerBase
    {

        private readonly OrganizacionRepository _repository;

        /// <summary>
        /// Constructor de OrganizacionController.
        /// </summary>
        /// <param name="repository">Instancia de OrganizacionRepository.</param>
        public OrganizacionController(OrganizacionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las organizaciones.
        /// </summary>
        /// <returns>Lista de organizaciones.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizaciones = await _repository.GetAllAsync();
            return Ok(organizaciones);
        }

        /// <summary>
        /// Obtiene una organizacion en particular por su id.
        /// </summary>
        /// <param name="id">Id de la organizacion.</param>
        /// <returns>Organizacion encontrada, o 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var organizacion = await _repository.GetByIdSync(id);
            if (organizacion == null)
            {
                return NotFound();
            }
            return Ok(organizacion);
        }

        /// <summary>
        /// Crea una nueva organizacion.
        /// </summary>
        /// <param name="organizacion">Objeto con datos de la organizacion.</param>
        /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Organizacion organizacion)
        {
            await _repository.AddAsync(organizacion);
            return CreatedAtAction(nameof(GetByID), new { id = organizacion.Id }, organizacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Organizacion organizacion)
        {
            if (id != organizacion.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(organizacion);
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