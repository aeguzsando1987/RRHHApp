using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;
using RRHH.WebApi.Models.Dtos.Ubicacion;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.JsonPatch;

namespace RRHH.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private readonly UbicacionRepository _repository;

        public UbicacionController(UbicacionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UbicacionReadDto>>> GetAll()
        {
            var ubicaciones = await _repository.GetAllAsync();
            var dtos = ubicaciones.Select(u => new UbicacionReadDto
            {
                ID = u.ID,
                Clave = u.Clave,
                Ubicacion_Referencial = u.Ubicacion_Referencial,
                Id_Empresa = u.Id_Empresa
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UbicacionReadDto>> GetById(int id)
        {
            var ubicacion = await _repository.GetByIdAsync(id);
            if (ubicacion == null) return NotFound();

            var dto = new UbicacionReadDto
            {
                ID = ubicacion.ID,
                Clave = ubicacion.Clave,
                Ubicacion_Referencial = ubicacion.Ubicacion_Referencial,
                Id_Empresa = ubicacion.Id_Empresa
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UbicacionReadDto>> Create([FromBody] UbicacionCreateDto dto)
        {
            var ubicacion = new Ubicacion
            {
                Clave = dto.Clave,
                Ubicacion_Referencial = dto.Ubicacion_Referencial,
                Id_Empresa = dto.Id_Empresa
            };

            await _repository.AddAsync(ubicacion);

            var readDto = new UbicacionReadDto
            {
                ID = ubicacion.ID,
                Clave = ubicacion.Clave,
                Ubicacion_Referencial = ubicacion.Ubicacion_Referencial,
                Id_Empresa = ubicacion.Id_Empresa
            };

            return CreatedAtAction(nameof(GetById), new { id = ubicacion.ID }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UbicacionUpdateDto dto)
        {
            var ubicacion = await _repository.GetByIdAsync(id);
            if (ubicacion == null) return NotFound();

            ubicacion.Clave = dto.Clave;
            ubicacion.Ubicacion_Referencial = dto.Ubicacion_Referencial;
            ubicacion.Id_Empresa = dto.Id_Empresa;

            await _repository.UpdateAsync(ubicacion);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(int id, [FromBody] JsonPatchDocument<UbicacionUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var ubicacion = await _repository.GetByIdAsync(id);
            if (ubicacion == null) return NotFound();

            var dto = new UbicacionUpdateDto
            {
                Clave = ubicacion.Clave,
                Ubicacion_Referencial = ubicacion.Ubicacion_Referencial,
                Id_Empresa = ubicacion.Id_Empresa
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ubicacion.Clave = dto.Clave;
            ubicacion.Ubicacion_Referencial = dto.Ubicacion_Referencial;
            ubicacion.Id_Empresa = dto.Id_Empresa;

            await _repository.UpdateAsync(ubicacion);

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