using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Empleado;
using RRHH.WebApi.Models.Dtos.EmpleadoPerfil;
using RRHH.WebApi.Repositories.Interfaces;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using RRHH.WebApi.Models.Dtos.EmpladoTipo;
using RRHH.WebApi.Services;

namespace RRHH.WebApi.Controllers
{

    /// <summary>
    /// Controlador para la creacion, edicion y eliminacion de empleados.
    /// </summary>
    // La ruta base de este controlador es "api/Empleado".
    // Todas las rutas definidas en este controlador seran "api/Empleado/<ruta-definida>".
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly IEmpleadoRepository _repository;
        private readonly EmpleadoTipoRepository _tipoRepository;
        private readonly IEmpleadoService _empleadoService;

        /// <summary>
        /// Constructor del controlador de Empleados.
        /// </summary>
        /// <param name="repository">Instancia de la interfaz de acceso a la base de datos.</param>
        /// <param name="tipoRepository">Instancia del repositorio de tipos de empleado.</param>
        /// <param name="empleadoService">Instancia del servicio de empleados.</param>
        public EmpleadoController(IEmpleadoRepository repository, EmpleadoTipoRepository tipoRepository, IEmpleadoService empleadoService)
        {
            _repository = repository;
            _tipoRepository = tipoRepository;
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoReadDto>>> GetAll()
        {
           var empleados = await _repository.GetAllAsync();
           var dtos = empleados.Select(e => new EmpleadoReadDto {
                ID = e.ID,
                Id_Status = e.Id_Status,
                Id_Puesto = e.Id_Puesto,
                Id_Jefe = e.Id_Jefe,
                Id_Ubicacion = e.Id_Ubicacion,
                Perfil = e.Perfil != null ? new EmpleadoPerfilReadDto {
                    Id_Empleado = e.Perfil.Id_Empleado,
                    Clave = e.Perfil.Clave,
                    Nombres = e.Perfil.Nombres,
                    Apellido_Paterno = e.Perfil.Apellido_Paterno,
                    Apellido_Materno = e.Perfil.Apellido_Materno,
                    Fecha_Nacimiento = e.Perfil.Fecha_Nacimiento,
                    Sexo = e.Perfil.Sexo,
                    Edo_Civil = e.Perfil.Edo_Civil,
                    Fecha_Inicio = e.Perfil.Fecha_Inicio,
                    Fecha_Termino = e.Perfil.Fecha_Termino,
                    Email = e.Perfil.Email,
                    Tel = e.Perfil.Tel,
                    NSS = e.Perfil.NSS,
                    RFC = e.Perfil.RFC,
                    Fotografia = e.Perfil.Fotografia,
                    Id_Tipo_Empleado = e.Perfil.Id_Tipo_Empleado,
                    Tipo_Empleado = e.Perfil.Tipo != null ? new EmpleadoTipoReadDto {
                        ID = e.Perfil.Tipo.ID,
                        Titulo = e.Perfil.Tipo.Titulo,
                        Descripcion = e.Perfil.Tipo.Descripcion,
                        Prefijo = e.Perfil.Tipo.Prefijo
                    } : null
                } : null
           });
           return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoReadDto>> GetById(int id)
        {
            var empleado = await _repository.GetByIdAsync(id);
            if(empleado == null) return NotFound();

            var dto = new EmpleadoReadDto{
                ID = empleado.ID,
                Id_Status = empleado.Id_Status,
                Id_Puesto = empleado.Id_Puesto,
                Id_Jefe = empleado.Id_Jefe,
                Id_Ubicacion = empleado.Id_Ubicacion,
                Perfil = empleado.Perfil != null ? new EmpleadoPerfilReadDto{
                    Id_Empleado = empleado.Perfil.Id_Empleado,
                    Clave = empleado.Perfil.Clave,
                    Nombres = empleado.Perfil.Nombres,
                    Apellido_Paterno = empleado.Perfil.Apellido_Paterno,
                    Apellido_Materno = empleado.Perfil.Apellido_Materno,
                    Fecha_Nacimiento = empleado.Perfil.Fecha_Nacimiento,
                    Sexo = empleado.Perfil.Sexo,
                    Edo_Civil = empleado.Perfil.Edo_Civil,
                    Fecha_Inicio = empleado.Perfil.Fecha_Inicio,
                    Fecha_Termino = empleado.Perfil.Fecha_Termino,
                    Email = empleado.Perfil.Email,
                    Tel = empleado.Perfil.Tel,
                    NSS = empleado.Perfil.NSS,
                    RFC = empleado.Perfil.RFC,
                    Fotografia = empleado.Perfil.Fotografia,
                    Id_Tipo_Empleado = empleado.Perfil.Id_Tipo_Empleado,
                    Tipo_Empleado = empleado.Perfil.Tipo != null ? new EmpleadoTipoReadDto {
                        ID = empleado.Perfil.Tipo.ID,
                        Titulo = empleado.Perfil.Tipo.Titulo,
                        Descripcion = empleado.Perfil.Tipo.Descripcion,
                        Prefijo = empleado.Perfil.Tipo.Prefijo
                    } : null
                } : null
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoReadDto>> Create([FromBody] EmpleadoCreateDto dto)
        {
           var empleado = new Empleado
           {
            Id_Status = dto.Id_Status,
            Id_Puesto = dto.Id_Puesto,
            Id_Jefe = dto.Id_Jefe,
            Id_Ubicacion = dto.Id_Ubicacion,
            Perfil = new Empleado_Perfil
            {
                Nombres = dto.Perfil.Nombres,
                Apellido_Paterno = dto.Perfil.Apellido_Paterno,
                Apellido_Materno = dto.Perfil.Apellido_Materno,
                Fecha_Nacimiento = dto.Perfil.Fecha_Nacimiento,
                Sexo = dto.Perfil.Sexo,
                Edo_Civil = dto.Perfil.Edo_Civil,
                Fecha_Inicio = dto.Perfil.Fecha_Inicio,
                Fecha_Termino = dto.Perfil.Fecha_Termino,
                Email = dto.Perfil.Email,
                Tel = dto.Perfil.Tel,
                NSS = dto.Perfil.NSS,
                RFC = dto.Perfil.RFC,
                Fotografia = dto.Perfil.Fotografia,
                Id_Tipo_Empleado = dto.Perfil.Id_Tipo_Empleado
            }
           };

           await _repository.AddAsync(empleado);

           var readDto = new EmpleadoReadDto
           {
               ID = empleado.ID,
               Id_Status = empleado.Id_Status,
               Id_Puesto = empleado.Id_Puesto,
               Id_Jefe = empleado.Id_Jefe,
               Id_Ubicacion = empleado.Id_Ubicacion,
               Perfil = new EmpleadoPerfilReadDto
               {
                   Id_Empleado = empleado.Perfil.Id_Empleado,
                   Clave = empleado.Perfil.Clave,
                   Nombres = empleado.Perfil.Nombres,
                   Apellido_Paterno = empleado.Perfil.Apellido_Paterno,
                   Apellido_Materno = empleado.Perfil.Apellido_Materno,
                   Fecha_Nacimiento = empleado.Perfil.Fecha_Nacimiento,
                   Sexo = empleado.Perfil.Sexo,
                   Edo_Civil = empleado.Perfil.Edo_Civil,
                   Fecha_Inicio = empleado.Perfil.Fecha_Inicio,
                   Fecha_Termino = empleado.Perfil.Fecha_Termino,
                   Email = empleado.Perfil.Email,
                   Tel = empleado.Perfil.Tel,
                   NSS = empleado.Perfil.NSS,
                   RFC = empleado.Perfil.RFC,
                   Fotografia = empleado.Perfil.Fotografia,
                   Id_Tipo_Empleado = empleado.Perfil.Id_Tipo_Empleado,
                   Tipo_Empleado = empleado.Perfil.Tipo != null ? new EmpleadoTipoReadDto {
                       ID = empleado.Perfil.Tipo.ID,
                       Titulo = empleado.Perfil.Tipo.Titulo,
                       Descripcion = empleado.Perfil.Tipo.Descripcion,
                       Prefijo = empleado.Perfil.Tipo.Prefijo
                   } : null
               }
           };
           return CreatedAtAction(nameof(GetById), new {id = empleado.ID}, readDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EmpleadoUpdateDto dto)
        {
           var empleado = await _repository.GetByIdAsync(id);
           if (empleado == null) return NotFound();

           if (empleado.Perfil == null)
           {
                empleado.Perfil = new Empleado_Perfil
                {
                    Id_Empleado = empleado.ID,
                    Nombres = dto.Perfil.Nombres,
                    Apellido_Paterno = dto.Perfil.Apellido_Paterno,
                    Apellido_Materno = dto.Perfil.Apellido_Materno,
                    Fecha_Nacimiento = dto.Perfil.Fecha_Nacimiento,
                    Sexo = dto.Perfil.Sexo,
                    Edo_Civil = dto.Perfil.Edo_Civil,
                    Fecha_Inicio = dto.Perfil.Fecha_Inicio ?? DateTime.Now,
                    Fecha_Termino = dto.Perfil.Fecha_Termino,
                    Email = dto.Perfil.Email,
                    Tel = dto.Perfil.Tel,
                    NSS = dto.Perfil.NSS,
                    RFC = dto.Perfil.RFC,
                    Fotografia = dto.Perfil.Fotografia,
                    Id_Tipo_Empleado = dto.Perfil.Id_Tipo_Empleado
                };
           }
           else
           {
                empleado.Perfil.Nombres = dto.Perfil.Nombres;
                empleado.Perfil.Apellido_Paterno = dto.Perfil.Apellido_Paterno;
                empleado.Perfil.Apellido_Materno = dto.Perfil.Apellido_Materno;
                empleado.Perfil.Fecha_Nacimiento = dto.Perfil.Fecha_Nacimiento;
                empleado.Perfil.Sexo = dto.Perfil.Sexo;
                empleado.Perfil.Edo_Civil = dto.Perfil.Edo_Civil;
                empleado.Perfil.Fecha_Inicio = dto.Perfil.Fecha_Inicio ?? DateTime.Now;
                empleado.Perfil.Fecha_Termino = dto.Perfil.Fecha_Termino;
                empleado.Perfil.Email = dto.Perfil.Email;
                empleado.Perfil.Tel = dto.Perfil.Tel;
                empleado.Perfil.NSS = dto.Perfil.NSS;
                empleado.Perfil.RFC = dto.Perfil.RFC;
                empleado.Perfil.Fotografia = dto.Perfil.Fotografia;
                empleado.Perfil.Id_Tipo_Empleado = dto.Perfil.Id_Tipo_Empleado;
           }

           await _repository.UpdateAsync(empleado);
           return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<EmpleadoUpdateDto> patchDoc)
        {
           if (patchDoc == null) return BadRequest();

           var empleado = await _repository.GetByIdAsync(id);
           if(empleado == null) return NotFound();

           var dto = new EmpleadoUpdateDto
           {
                Id_Status = empleado.Id_Status,
                Id_Puesto = empleado.Id_Puesto,
                Id_Jefe = empleado.Id_Jefe,
                Id_Ubicacion = empleado.Id_Ubicacion,
                Perfil = empleado.Perfil != null ? new EmpleadoPerfilUpdateDto
                {
                    Nombres = empleado.Perfil.Nombres,
                    Apellido_Paterno = empleado.Perfil.Apellido_Paterno,
                    Apellido_Materno = empleado.Perfil.Apellido_Materno,
                    Fecha_Nacimiento = empleado.Perfil.Fecha_Nacimiento,
                    Sexo = empleado.Perfil.Sexo,
                    Edo_Civil = empleado.Perfil.Edo_Civil,
                    Fecha_Inicio = empleado.Perfil.Fecha_Inicio,
                    Fecha_Termino = empleado.Perfil.Fecha_Termino,
                    Email = empleado.Perfil.Email,
                    Tel = empleado.Perfil.Tel,
                    NSS = empleado.Perfil.NSS,
                    RFC = empleado.Perfil.RFC,
                    Fotografia = empleado.Perfil.Fotografia,
                    Id_Tipo_Empleado = empleado.Perfil.Id_Tipo_Empleado
                } : null
           };

           patchDoc.ApplyTo(dto, ModelState);
           if (!ModelState.IsValid) return BadRequest(ModelState);

           empleado.Id_Status = dto.Id_Status;
           empleado.Id_Puesto = dto.Id_Puesto;
           empleado.Id_Jefe = dto.Id_Jefe;
           empleado.Id_Ubicacion = dto.Id_Ubicacion;

           if (dto.Perfil != null)
           {
                if (empleado.Perfil == null)
                {
                    empleado.Perfil = new Empleado_Perfil
                    {
                        Id_Empleado = empleado.ID,
                        Nombres = dto.Perfil.Nombres,
                        Apellido_Paterno = dto.Perfil.Apellido_Paterno,
                        Apellido_Materno = dto.Perfil.Apellido_Materno,
                        Fecha_Nacimiento = dto.Perfil.Fecha_Nacimiento,
                        Sexo = dto.Perfil.Sexo,
                        Edo_Civil = dto.Perfil.Edo_Civil,
                        Fecha_Inicio = dto.Perfil.Fecha_Inicio ?? DateTime.Now,
                        Fecha_Termino = dto.Perfil.Fecha_Termino,
                        Email = dto.Perfil.Email,
                        Tel = dto.Perfil.Tel,
                        NSS = dto.Perfil.NSS,
                        RFC = dto.Perfil.RFC,
                        Fotografia = dto.Perfil.Fotografia,
                        Id_Tipo_Empleado = dto.Perfil.Id_Tipo_Empleado
                    };
                }
                else
                {
                    empleado.Perfil.Nombres = dto.Perfil.Nombres;
                    empleado.Perfil.Apellido_Paterno = dto.Perfil.Apellido_Paterno;
                    empleado.Perfil.Apellido_Materno = dto.Perfil.Apellido_Materno;
                    empleado.Perfil.Fecha_Nacimiento = dto.Perfil.Fecha_Nacimiento;
                    empleado.Perfil.Sexo = dto.Perfil.Sexo;
                    empleado.Perfil.Edo_Civil = dto.Perfil.Edo_Civil;
                    empleado.Perfil.Fecha_Inicio = dto.Perfil.Fecha_Inicio ?? DateTime.Now;
                    empleado.Perfil.Fecha_Termino = dto.Perfil.Fecha_Termino;
                    empleado.Perfil.Email = dto.Perfil.Email;
                    empleado.Perfil.Tel = dto.Perfil.Tel;
                    empleado.Perfil.NSS = dto.Perfil.NSS;
                    empleado.Perfil.RFC = dto.Perfil.RFC;
                    empleado.Perfil.Fotografia = dto.Perfil.Fotografia;
                    empleado.Perfil.Id_Tipo_Empleado = dto.Perfil.Id_Tipo_Empleado;
                }
           }

           await _repository.UpdateAsync(empleado);
           return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateEmpleadoStatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _empleadoService.UpdateEmpleadoStatusAsync(id, statusDto.NewStatusId);
            if (!success)
            {
                var empleadoExists = await _repository.GetByIdAsync(id);
                if (empleadoExists == null)
                {
                    return NotFound(new { Message = $"Empleado con ID {id} no encontrado"});
                }

                return StatusCode(StatusCodes.Status500InternalServerError, 
                                new { Message = "Error al actualizar el estatus del empleado"});
            }

            return NoContent();
        }

    }
}