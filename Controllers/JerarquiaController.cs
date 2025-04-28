using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Jerarquia;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace RRHH.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JerarquiaController : ControllerBase
    {
        private readonly JerarquiaRepository _repository;

        public JerarquiaController(JerarquiaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las jerarquias de la base de datos.
        /// </summary>
        /// <returns>Lista de jerarquias.</returns>
        /// Esta funcion llama a GetAllAsync de la interfaz de la base de datos,
        /// que devuelve una lista de todas las jerarquias.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JerarquiaReadDto>>> GetAll()
        {
            var jerarquias = await _repository.GetAllAsync(); // Llama a la implementacion de la interfaz de la base de datos
            var dtos = jerarquias.Select(j => new JerarquiaReadDto
            {
                // Crea un objeto Dto con los datos de la jerarquia
                ID = j.ID,
                Clave = j.Clave,
                Titulo = j.Titulo,
                Nivel = j.Nivel,
                Descripcion = j.Descripcion
            });
            return Ok(dtos);
        }

        /// <summary>
        /// Obtiene una jerarquia en particular por su id.
        /// </summary>
        /// <param name="id">Id de la jerarquia.</param>
        /// <returns>Jerarquia encontrada, o 404 si no existe.</returns>
        /// Esta funcion llama a GetByIdSync de la interfaz de la base de datos,
        /// que devuelve una jerarquia en particular por su id.
        [HttpGet("{id}")]
        public async Task<ActionResult<JerarquiaReadDto>> GetByID(int id)
        {
            var j = await _repository.GetByIdAsync(id); // Llama a la implementacion de la interfaz de la base de datos
            if (j == null) return NotFound();
            var dto = new JerarquiaReadDto
            {
                // Crea un objeto Dto con los datos de la jerarquia
                ID = j.ID,
                Clave = j.Clave,
                Titulo = j.Titulo,
                Nivel = j.Nivel,
                Descripcion = j.Descripcion
            };
            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva jerarquia.
        /// </summary>
        /// <param name="jerarquia">Objeto con datos de la jerarquia.</param>
        /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
        /// Esta funcion llama a AddAsync de la interfaz de la base de datos,
        /// que se encarga de agregar una nueva jerarquia a la base de datos.
        [HttpPost]
        public async Task<ActionResult<JerarquiaReadDto>> Create(JerarquiaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var j = new Jerarquia
            {
                Clave = dto.Clave,
                Titulo = dto.Titulo,
                Nivel = dto.Nivel,
                Descripcion = dto.Descripcion
            };
            await _repository.AddAsync(j);

            var readDto = new JerarquiaReadDto
            {
                ID = j.ID,
                Clave = j.Clave,
                Titulo = j.Titulo,
                Descripcion = j.Descripcion
            };
            return CreatedAtAction(nameof(GetByID), new { id = j.ID }, readDto);
        }

        /// <summary>
        /// Actualiza una jerarquia existente en la base de datos.
        /// </summary>
        /// <param name="id">Id de la jerarquia a actualizar.</param>
        /// <param name="jerarquia">Objeto con datos de la jerarquia.</param>
        /// <returns>204 No Content</returns>
        /// Esta funcion llama a UpdateAsync de la interfaz de la base de datos,
        /// que se encarga de actualizar una jerarquia existente en la base de datos.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JerarquiaUpdateDto dto)
        {
            var j = await _repository.GetByIdAsync(id);
            if (j == null) return NotFound();

            j.Clave = dto.Clave;
            j.Titulo = dto.Titulo;
            j.Nivel = dto.Nivel;
            j.Descripcion = dto.Descripcion;

            await _repository.UpdateAsync(j);
            return NoContent();
        }

        /// <summary>
        /// Actualiza una jerarquia existente en la base de datos mediante un JSON Patch
        /// </summary>
        /// <param name="id">Id de la jerarquia a actualizar</param>
        /// <param name="patchDoc">Documento JSON Patch con los cambios a realizar</param>
        /// <returns>204 No Content</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<JerarquiaUpdateDto> patchDoc)
        {
            // Verifica que el patchDoc no sea nulo
            if (patchDoc == null)
                return BadRequest();

            // Busca la jerarquia por su id
            var jerarquia = await _repository.GetByIdAsync(id);

            // Si la jerarquia no existe, regresa 404 Not Found
            if (jerarquia == null)
                return NotFound();

            // Crea un objeto DTO con los datos de la jerarquia encontrada
            var dto = new JerarquiaUpdateDto
            {
                Clave = jerarquia.Clave,
                Titulo = jerarquia.Titulo,
                Nivel = jerarquia.Nivel,
                Descripcion = jerarquia.Descripcion
            };

            // Aplica el patch al objeto DTO y valida el modelo
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Asigna los valores actualizados del DTO a la jerarquia encontrada
            jerarquia.Clave = dto.Clave;
            jerarquia.Titulo = dto.Titulo;
            jerarquia.Nivel = dto.Nivel;
            jerarquia.Descripcion = dto.Descripcion;
            // Actualiza la jerarquia en la base de datos
            await _repository.UpdateAsync(jerarquia);

            // Regresa 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Elimina una jerarquia por su id
        /// </summary>
        /// <param name="id">Id de la jerarquia a eliminar</param>
        /// <returns>204 No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca la jerarquia por su id
            await _repository.DeleteAsync(id);
            // Si se encuentra y se elimina, devuelve 204 No Content
            return NoContent();
        }
    }
}