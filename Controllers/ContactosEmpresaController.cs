using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.ContactosEmpresa;
using RRHH.WebApi.Repositories;
using Microsoft.JSInterop.Infrastructure;

namespace RRHH.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosEmpresaController : ControllerBase
    {
        private readonly ContactosEmpresaRepository _repository;

        public ContactosEmpresaController(ContactosEmpresaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los contactos de la base de datos.
        /// </summary>
        /// <returns>Lista de contactos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactosEmpresaReadDto>>> GetAll()
        {
            // Llamar a la interfaz de la base de datos para obtener todos los contactos.
            var contactos = await _repository.GetAllAsync();
            // Mapear los contactos a DTO para enviar al cliente.
            var dtos = contactos.Select(a => new ContactosEmpresaReadDto {
                ID = a.ID,
                Id_Empresa = a.Id_Empresa,
                Nombre_Alias = a.Nombre_Alias,
                Domicilio = a.Domicilio,
                Telefono = a.Telefono,
                Email = a.Email,
                Puesto_Ref = a.Puesto_Ref
            });
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactosEmpresaReadDto>> GetById(int id)
        {
            // Llamar a la interfaz de la base de datos para obtener el contacto por su id.
            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null)
            {
                // Si no se encuentra el contacto, devolver un 404.
                return NotFound();
            }
            // Mapear el contacto a DTO para enviar al cliente.
            var dto = new ContactosEmpresaReadDto {
                ID = contacto.ID,
                Id_Empresa = contacto.Id_Empresa,
                Nombre_Alias = contacto.Nombre_Alias,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Puesto_Ref = contacto.Puesto_Ref
            };
            // Enviar el resultado al cliente con un 200 Ok.
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ContactosEmpresaReadDto>> Create(ContactosEmpresaCreateDto dto)
        {
            // Crear una nueva contacto con los datos del dto.
            var contacto = new ContactosEmpresa 
            {
                Id_Empresa = dto.Id_Empresa,
                Nombre_Alias = dto.Nombre_Alias,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Puesto_Ref = dto.Puesto_Ref
            };
            // Agregar el contacto a la base de datos.
            await _repository.AddAsync(contacto);
            // Mapear el contacto a DTO para enviar al cliente.
            var readDto = new ContactosEmpresaReadDto {
                ID = contacto.ID,
                Id_Empresa = contacto.Id_Empresa,
                Nombre_Alias = contacto.Nombre_Alias,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Puesto_Ref = contacto.Puesto_Ref
            };
            // Enviar el resultado al cliente con un 201 Created.
            return CreatedAtAction(nameof(GetById), new { id = contacto.ID }, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContactosEmpresaUpdateDto dto)
        {
            // Buscar el contacto a actualizar por su id.
            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null) return NotFound();

            // Asignar solo los campos del dto.
            contacto.Id_Empresa = dto.Id_Empresa;
            contacto.Nombre_Alias = dto.Nombre_Alias;
            contacto.Domicilio = dto.Domicilio;
            contacto.Telefono = dto.Telefono;
            contacto.Email = dto.Email;
            contacto.Puesto_Ref = dto.Puesto_Ref;

            // Actualizar el contacto en la base de datos.
            await _repository.UpdateAsync(contacto);
            // Enviar un 204 No Content.
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ContactosEmpresaUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var contacto = await _repository.GetByIdAsync(id);
            if (contacto == null) return NotFound();

            var dto = new ContactosEmpresaUpdateDto
            {
                Id_Empresa = contacto.Id_Empresa,
                Nombre_Alias = contacto.Nombre_Alias,
                Domicilio = contacto.Domicilio,
                Telefono = contacto.Telefono,
                Email = contacto.Email,
                Puesto_Ref = contacto.Puesto_Ref
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            contacto.Id_Empresa = dto.Id_Empresa;
            contacto.Nombre_Alias = dto.Nombre_Alias;
            contacto.Domicilio = dto.Domicilio;
            contacto.Telefono = dto.Telefono;
            contacto.Email = dto.Email;
            contacto.Puesto_Ref = dto.Puesto_Ref;

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

