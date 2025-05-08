using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.ContactosEmpleado;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de contactos de empleados.
    /// </summary>          
    /// <remarks>
    /// El controlador se encarga de recibir las solicitudes HTTP
    /// y de llamar a la interfaz de la base de datos para obtener
    /// y manipular los contactos de empleados.
    /// </remarks>
    // La ruta base de este controlador es "api/ContactosEmpleado".
    // Todas las rutas definidas en este controlador seran "api/ContactosEmpleado/<ruta-definida>".
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosEmpleadoController : ControllerBase
    {
        private readonly ContactosEmpleadoRepository _repository;

        public ContactosEmpleadoController(ContactosEmpleadoRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los contactos de la base de datos.
        /// </summary>
        /// <returns>Lista de contactos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactosEmpleadoReadDto>>> GetAll()
        {
            // Llamar a la interfaz de la base de datos para obtener todos los contactos.
            var contactos = await _repository.GetAllAsync();
            // Mapear los contactos a DTO para enviar al cliente.
            var dtos = contactos.Select(a => new ContactosEmpleadoReadDto {
                ID = a.ID,
                Id_Empleado = a.Id_Empleado,
                Nombre_Contacto = a.Nombre_Contacto,
                Domicilio = a.Domicilio,
                Telefono = a.Telefono,
                Email = a.Email,
                Relacion = a.Relacion
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactosEmpleadoReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener el contacto por su id.
            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null)
            {
                // Si no se encuentra el contacto, devolver un 404.
                return NotFound();
            }
            // Mapear el contacto a DTO para enviar al cliente.
            var dto = new ContactosEmpleadoReadDto {
                ID = contacto.ID,
                Id_Empleado = contacto.Id_Empleado,
                Nombre_Contacto = contacto.Nombre_Contacto,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Relacion = contacto.Relacion
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactosEmpleadoReadDto>> Create(ContactosEmpleadoCreateDto dto)
        {
            // Crear una nueva contacto con los datos del dto.
            var contacto = new ContactosEmpleado 
            {
                Id_Empleado = dto.Id_Empleado,
                Nombre_Contacto = dto.Nombre_Contacto,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Relacion = dto.Relacion
            };
            // Agregar el contacto a la base de datos.
            await _repository.AddAsync(contacto);
            // Mapear el contacto a DTO para enviar al cliente.
            var readDto = new ContactosEmpleadoReadDto {
                ID = contacto.ID,
                Id_Empleado = contacto.Id_Empleado,
                Nombre_Contacto = contacto.Nombre_Contacto,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Relacion = contacto.Relacion
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new { id = contacto.ID }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContactosEmpleadoUpdateDto dto)
        {
            // Buscar el contacto a actualizar por su id.
            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null) return NotFound();

            // Asignar solo los campos del dto.
            contacto.Id_Empleado = dto.Id_Empleado;
            contacto.Nombre_Contacto = dto.Nombre_Contacto;
            contacto.Domicilio = dto.Domicilio;
            contacto.Telefono = dto.Telefono;
            contacto.Email = dto.Email;
            contacto.Relacion = dto.Relacion;

            // Actualizar el contacto en la base de datos.
            await _repository.UpdateAsync(contacto);
            // Enviar un 204 No Content.
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ContactosEmpleadoUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null) return NotFound();

            var dto = new ContactosEmpleadoUpdateDto
            {
                Id_Empleado = contacto.Id_Empleado,
                Nombre_Contacto = contacto.Nombre_Contacto,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Relacion = contacto.Relacion
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            contacto.Id_Empleado = dto.Id_Empleado;
            contacto.Nombre_Contacto = dto.Nombre_Contacto;
            contacto.Domicilio = dto.Domicilio;
            contacto.Telefono = dto.Telefono;
            contacto.Email = dto.Email;
            contacto.Relacion = dto.Relacion;

            await _repository.UpdateAsync(contacto);
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