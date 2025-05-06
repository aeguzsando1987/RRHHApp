using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories;
using RRHH.WebApi.Models.Dtos.EmpladoTipo;
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
    public class EmpleadoTipoController : ControllerBase
    {
        private readonly EmpleadoTipoRepository _repository;

        public EmpleadoTipoController(EmpleadoTipoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoTipoReadDto>>> GetAll()
        {
            var tipos = await _repository.GetAllAsync();
            var dtos = tipos.Select(t => new EmpleadoTipoReadDto
            {
                ID = t.ID,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Prefijo = t.Prefijo
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoTipoReadDto>> GetById(int id)
        {
            var tipo = await _repository.GetByIdAsync(id);
            if (tipo == null) return NotFound();

            var dto = new EmpleadoTipoReadDto
            {
                ID = tipo.ID,
                Titulo = tipo.Titulo,
                Descripcion = tipo.Descripcion,
                Prefijo = tipo.Prefijo
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoTipoReadDto>> Create( [FromBody] EmpleadoTipoCreateDto dto)
        {
           var tipo = new Empleado_Tipo
           {
            Titulo = dto.Titulo,
            Descripcion = dto.Descripcion,
            Prefijo = dto.Prefijo
           };

           await _repository.AddAsync(tipo);

           var readDto = new EmpleadoTipoReadDto
           {
            ID = tipo.ID,
            Titulo = tipo.Titulo,
            Descripcion = tipo.Descripcion,
            Prefijo = tipo.Prefijo
           };

           return CreatedAtAction(nameof(GetById), new { id = tipo.ID }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoTipoUpdateDto dto)
        {
            var tipo = await _repository.GetByIdAsync(id);
            if (tipo == null) return NotFound();

            tipo.Titulo = dto.Titulo;
            tipo.Descripcion = dto.Descripcion;
            tipo.Prefijo = dto.Prefijo;

            await _repository.UpdateAsync(tipo);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdate(int id, [FromBody] JsonPatchDocument<EmpleadoTipoUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var tipo = await _repository.GetByIdAsync(id);
            if (tipo == null) return NotFound();

            var dto = new EmpleadoTipoUpdateDto
            {
                Titulo = tipo.Titulo,
                Descripcion = tipo.Descripcion,
                Prefijo = tipo.Prefijo
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            tipo.Titulo = dto.Titulo;
            tipo.Descripcion = dto.Descripcion;
            tipo.Prefijo = dto.Prefijo;

            await _repository.UpdateAsync(tipo);

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