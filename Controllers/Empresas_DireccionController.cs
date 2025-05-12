using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Empresas_Direccion;
using RRHH.WebApi.Repositories;
using RRHH.WebApi.Models.Dtos.Empleados_Direccion;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Empresas_DireccionController : ControllerBase
    {
        private readonly IEmpresas_DireccionRepository _repository;

        public Empresas_DireccionController(IEmpresas_DireccionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresasDireccionReadDto>>> GetAll()
        {
            var empresasDirecciones = await _repository.GetAllAsync();
            var dtos = empresasDirecciones.Select(a => new EmpresasDireccionReadDto {
                ID = a.ID,
                Id_Empresa = a.Id_Empresa,
                Clave = a.Clave,
                Calle = a.Calle,
                Numero_Ext = a.Numero_Ext,
                Numero_Int = a.Numero_Int,
                Colonia = a.Colonia,
                Municipio = a.Municipio,
                Estado = a.Estado,
                Codigo_Postal = a.Codigo_Postal,
                Pais = a.Pais,
                Referencia = a.Referencia,
                Fecha_Modificacion = a.Fecha_Modificacion
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresasDireccionReadDto>> GetById(int id)
        {
            var empresaDireccion = await _repository.GetByIdAsync(id);
            if (empresaDireccion == null)
            {
                return NotFound();
            }
            var dto = new EmpresasDireccionReadDto {
                ID = empresaDireccion.ID,
                Id_Empresa = empresaDireccion.Id_Empresa,
                Clave = empresaDireccion.Clave,
                Calle = empresaDireccion.Calle,
                Numero_Ext = empresaDireccion.Numero_Ext,
                Numero_Int = empresaDireccion.Numero_Int,
                Colonia = empresaDireccion.Colonia,
                Municipio = empresaDireccion.Municipio,
                Estado = empresaDireccion.Estado,
                Codigo_Postal = empresaDireccion.Codigo_Postal,
                Pais = empresaDireccion.Pais,
                Referencia = empresaDireccion.Referencia,
                Fecha_Modificacion = empresaDireccion.Fecha_Modificacion
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpresasDireccionReadDto>> Create(EmpresasDireccionCreateDto dto)
        {
            var direccion = new Empresas_Direccion
            {
                Id_Empresa = dto.Id_Empresa,
                Clave = dto.Clave,
                Calle = dto.Calle,
                Numero_Ext = dto.Numero_Ext,
                Numero_Int = dto.Numero_Int,
                Colonia = dto.Colonia,
                Municipio = dto.Municipio,
                Estado = dto.Estado,
                Codigo_Postal = dto.Codigo_Postal,
                Pais = dto.Pais,
                Referencia = dto.Referencia
            };
            await _repository.AddAsync(direccion);

            var readDto = new EmpresasDireccionReadDto
            {
                ID = direccion.ID,
                Id_Empresa = direccion.Id_Empresa,
                Clave = direccion.Clave,
                Calle = direccion.Calle,
                Numero_Ext = direccion.Numero_Ext,
                Numero_Int = direccion.Numero_Int,
                Colonia = direccion.Colonia,
                Municipio = direccion.Municipio,
                Estado = direccion.Estado,
                Codigo_Postal = direccion.Codigo_Postal,
                Pais = direccion.Pais,
                Referencia = direccion.Referencia,
                Fecha_Modificacion = direccion.Fecha_Modificacion
            };
            return CreatedAtAction(nameof(GetById), new { id = direccion.ID }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmpresasDireccionUpdateDto dto)
        {
            var direccion = await _repository.GetByIdAsync(id);
            if (direccion == null) return NotFound();

            direccion.Clave = dto.Clave;
            direccion.Calle = dto.Calle;
            direccion.Numero_Ext = dto.Numero_Ext;
            direccion.Numero_Int = dto.Numero_Int;
            direccion.Colonia = dto.Colonia;
            direccion.Municipio = dto.Municipio;
            direccion.Estado = dto.Estado;
            direccion.Codigo_Postal = dto.Codigo_Postal;
            direccion.Pais = dto.Pais;
            direccion.Referencia = dto.Referencia;

            await _repository.UpdateAsync(direccion);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<EmpresasDireccionUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var direccion = await _repository.GetByIdAsync(id);
            if (direccion == null) return NotFound();

            var dto = new EmpresasDireccionUpdateDto
            {
                Clave = direccion.Clave,
                Calle = direccion.Calle,
                Numero_Ext = direccion.Numero_Ext,
                Numero_Int = direccion.Numero_Int,
                Colonia = direccion.Colonia,
                Municipio = direccion.Municipio,
                Estado = direccion.Estado,
                Codigo_Postal = direccion.Codigo_Postal,
                Pais = direccion.Pais,
                Referencia = direccion.Referencia
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            direccion.Clave = dto.Clave;
            direccion.Calle = dto.Calle;
            direccion.Numero_Ext = dto.Numero_Ext;
            direccion.Numero_Int = dto.Numero_Int;
            direccion.Colonia = dto.Colonia;
            direccion.Municipio = dto.Municipio;
            direccion.Estado = dto.Estado;
            direccion.Codigo_Postal = dto.Codigo_Postal;
            direccion.Pais = dto.Pais;
            direccion.Referencia = dto.Referencia;

            await _repository.UpdateAsync(direccion);
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