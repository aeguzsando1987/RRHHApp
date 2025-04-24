using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Empresa;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Azure;
using Microsoft.AspNetCore.JsonPatch;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de empresas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly EmpresasRepository _repository;

        /// <summary>
        /// Constructor del controlador de Empresas.
        /// </summary>
        /// <param name="repository">Instancia de la interfaz de acceso a la base de datos.</param>
        public EmpresaController(EmpresasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empresas = await _repository.GetAllAsync();
            return Ok(empresas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return Ok(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            await _repository.AddSync(empresa);
            return CreatedAtAction(nameof(GetById), new {id = empresa.ID}, empresa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmpresaUpdateDto dto)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null) return NotFound();

            // Asignar solo los campos del dto
            empresa.Clave = dto.Clave;
            empresa.Razon_Social = dto.Razon_Social;
            empresa.RFC = dto.RFC;
            empresa.Direccion = dto.Direccion;
            empresa.Fecha_Creacion = dto.Fecha_Creacion;

            await _repository.UpdateAsync(empresa);
            return NoContent();

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<EmpresaUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
                return NotFound();

            // Mapear entidad a DTO
            var dto = new EmpresaUpdateDto
            {
                Clave = empresa.Clave,
                Razon_Social = empresa.Razon_Social,
                RFC = empresa.RFC,
                Direccion = empresa.Direccion,
                Fecha_Creacion = empresa.Fecha_Creacion
            };

            // Aplicar parche y validar modelo
            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Mapear de vuelta a la entidad
            empresa.Clave = dto.Clave;
            empresa.Razon_Social = dto.Razon_Social;
            empresa.RFC = dto.RFC;
            empresa.Direccion = dto.Direccion;
            empresa.Fecha_Creacion = dto.Fecha_Creacion;

            await _repository.UpdateAsync(empresa);
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