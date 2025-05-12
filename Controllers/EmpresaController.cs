using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos;
using RRHH.WebApi.Models.Dtos.Empresa;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Controllers
{
    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de empresas.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        private readonly IEmpresaRepository _repository;

        /// <summary>
        /// Constructor del controlador de Empresas.
        /// </summary>
        /// <param name="repository">Instancia de la interfaz de acceso a la base de datos.</param>
        public EmpresaController(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetAll()
        {
            var empresas = await _repository.GetAllAsync();
            var dtos = empresas.Select(e => new EmpresaReadDto {
                Id = e.ID,
                Id_Org = e.Id_Org,
                Clave = e.Clave,
                Razon_Social = e.Razon_Social,
                RFC = e.RFC,
                Direccion = e.Direccion,
                Fecha_Creacion = e.Fecha_Creacion
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaReadDto>> GetById(int id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            var dto = new EmpresaReadDto {
                Id = empresa.ID,
                Id_Org = empresa.Id_Org,
                Clave = empresa.Clave,
                Razon_Social = empresa.Razon_Social,
                RFC = empresa.RFC,
                Direccion = empresa.Direccion,
                Fecha_Creacion = empresa.Fecha_Creacion
            };
            return Ok(dto);
        }

        /// <summary>
        /// Crea una nueva empresa en la base de datos.
        /// </summary>
        /// <param name="dto">Objeto con datos de la empresa.</param>
        /// <returns>Objeto creado, con su id, o 400 si no es posible crearlo.</returns>
        [HttpPost]
        public async Task<ActionResult<EmpresaReadDto>> Create(EmpresaCreateDto dto)
        {
            // Crear una nueva empresa con los datos del dto.
            // Se crea un objeto Empresa con los datos del dto
            // para poder agregarlo a la base de datos.
            var empresa = new Empresa 
            {
                Id_Org = dto.Id_Org,
                Clave = dto.Clave,
                Razon_Social = dto.Razon_Social,
                RFC = dto.RFC,
                Direccion = dto.Direccion,
                Fecha_Creacion = dto.Fecha_Creacion
            };

            // Agrega la empresa a la base de datos.
            // Se agrega la empresa recien creada a la base de datos
            // para que pueda ser utilizada en el futuro.
            await _repository.AddAsync(empresa);

            // Mapea la empresa a DTO para enviar al cliente.
            // Se crea un objeto Dto con los datos de la empresa para
            // enviar al cliente. Se envia un objeto Dto en lugar de
            // un objeto Empresa para que el cliente no tenga acceso
            // a la informacion de la base de datos.
            var readDto = new EmpresaReadDto
            {
                // Crea un objeto Dto con los datos de la empresa
                Id = empresa.ID,
                Id_Org = empresa.Id_Org,
                Clave = empresa.Clave,
                Razon_Social = empresa.Razon_Social,
                RFC = empresa.RFC,
                Direccion = empresa.Direccion,
                Fecha_Creacion = empresa.Fecha_Creacion
            };

            // Enviar el resultado al cliente con un 201 Created.
            // La clase ActionResult es una clase abstracta que se encarga de 
            // representar el resultado de la accion. En este caso, se devuelve 
            // un 201 Created con los datos de la empresa recien creada.
            // La clase EmpresaReadDto es el tipo de objeto que se va a 
            // devolver al cliente. En este caso, se devuelve un objeto con 
            // los datos de la empresa recien creada.
            return CreatedAtAction(nameof(GetById), new {id = empresa.ID}, readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmpresaUpdateDto dto)
        {
            // Busca la empresa por su id y si no existe, regresa un 404 Not Found.
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null) return NotFound();

            // Asigna los valores del dto a la empresa encontrada. Se asigna solo los 
            // campos que se van a actualizar, los demas se quedan con los valores 
            // actuales.
            empresa.Clave = dto.Clave;
            empresa.Razon_Social = dto.Razon_Social;
            empresa.RFC = dto.RFC;
            empresa.Direccion = dto.Direccion;
            empresa.Fecha_Creacion = dto.Fecha_Creacion;

            // Actualiza la empresa en la base de datos.
            await _repository.UpdateAsync(empresa);

            // Regresa un 204 No Content para indicar que la peticion se ha 
            // realizado correctamente.
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<EmpresaUpdateDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null) return NotFound();

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
            if (!ModelState.IsValid)return BadRequest(ModelState);

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