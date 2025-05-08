using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Empleados_Direccion;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Empleados_DireccionController : ControllerBase
    {
        private readonly Empleados_DireccionRepository _repository;

        public Empleados_DireccionController(Empleados_DireccionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los empleados direcciones de la base de datos.
        /// </summary>
        /// <returns>Lista de empleados direcciones.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadosDireccionReadDto>>> GetAll()
        {
            // Llamar a la interfaz de la base de datos para obtener todos los empleados direcciones.
            var empleadosDirecciones = await _repository.GetAllAsync();
            // Mapear los empleados direcciones a DTO para enviar al cliente.
            var dtos = empleadosDirecciones.Select(a => new EmpleadosDireccionReadDto {
                ID = a.ID,
                Id_Empleado = a.Id_Empleado,
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
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadosDireccionReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener el empleado direccion por su id.
            var empleadoDireccion = await _repository.GetByIdAsync(id);
            if (empleadoDireccion == null)
            {
                // Si no se encuentra el empleado direccion, devolver un 404.
                return NotFound();
            }
            // Mapear el empleado direccion a DTO para enviar al cliente.
            var dto = new EmpleadosDireccionReadDto {
                ID = empleadoDireccion.ID,
                Id_Empleado = empleadoDireccion.Id_Empleado,
                Clave = empleadoDireccion.Clave,
                Calle = empleadoDireccion.Calle,
                Numero_Ext = empleadoDireccion.Numero_Ext,
                Numero_Int = empleadoDireccion.Numero_Int,
                Colonia = empleadoDireccion.Colonia,
                Municipio = empleadoDireccion.Municipio,
                Estado = empleadoDireccion.Estado,
                Codigo_Postal = empleadoDireccion.Codigo_Postal,
                Pais = empleadoDireccion.Pais,
                Referencia = empleadoDireccion.Referencia,
                Fecha_Modificacion = empleadoDireccion.Fecha_Modificacion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadosDireccionReadDto>> Create(EmpleadosDireccionCreateDto dto)
        {
            var direccion = new Empleados_Direccion
            {
                Id_Empleado = dto.Id_Empleado,
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

            var readDto = new EmpleadosDireccionReadDto
            {
                ID = direccion.ID,
                Id_Empleado = direccion.Id_Empleado,
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
        public async Task<IActionResult> Update(int id, EmpleadosDireccionUpdateDto dto)
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
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<EmpleadosDireccionUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var direccion = await _repository.GetByIdAsync(id);
            if (direccion == null) return NotFound();

            var dto = new EmpleadosDireccionUpdateDto
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
