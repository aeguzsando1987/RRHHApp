using Microsoft.AspNetCore.Mvc;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Dtos.Empleado;
using RRHH.WebApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;


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


        private readonly EmpleadoRepository _repository;

        /// <summary>
        /// Constructor del controlador de Empleados.
        /// </summary>
        /// <param name="repository">Instancia de la interfaz de acceso a la base de datos.</param>
        public EmpleadoController(EmpleadoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoReadDto>>> GetAll()
        {
            var empleados = await _repository.GetAllAsync();
            var dtos = empleados.Select(e => new EmpleadoReadDto {
               ID = e.ID,
               Clave = e.Clave,
               Nombres = e.Nombres,
               Apellido_Paterno = e.Apellido_Paterno,
               Apellido_Materno = e.Apellido_Materno,
               Fecha_Nacimiento = e.Fecha_Nacimiento,
               Fecha_Inicio = e.Fecha_Inicio,
               Fecha_Termino = e.Fecha_Termino,
               Email_corporativo = e.Email_corporativo,
               Tel = e.Tel,
               NSS = e.NSS,
               RFC = e.RFC,
               Id_Status = e.Id_Status,
               Id_Puesto = e.Id_Puesto,
               Id_Jefe = e.Id_Jefe,
               Id_Ubicacion = e.Id_Ubicacion,
               Fotografia = e.Fotografia
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
                Clave = empleado.Clave,
                Nombres = empleado.Nombres,
                Apellido_Paterno = empleado.Apellido_Paterno,
                Apellido_Materno = empleado.Apellido_Materno,
                Fecha_Nacimiento = empleado.Fecha_Nacimiento,
                Fecha_Inicio = empleado.Fecha_Inicio,
                Fecha_Termino = empleado.Fecha_Termino,
                Email_corporativo = empleado.Email_corporativo,
                Tel = empleado.Tel,
                NSS = empleado.NSS,
                RFC = empleado.RFC,
                Id_Status = empleado.Id_Status,
                Id_Puesto = empleado.Id_Puesto,
                Id_Jefe = empleado.Id_Jefe,
                Id_Ubicacion = empleado.Id_Ubicacion,
                Fotografia = empleado.Fotografia
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoReadDto>> Create([FromBody] EmpleadoCreateDto dto)
        {
            var empleado = new Empleado
            {
                Clave = dto.Clave,
                Nombres = dto.Nombres,
                Apellido_Paterno = dto.Apellido_Paterno,
                Apellido_Materno = dto.Apellido_Materno,
                Fecha_Nacimiento = dto.Fecha_Nacimiento,
                Fecha_Inicio = dto.Fecha_Inicio,
                Fecha_Termino = dto.Fecha_Termino,
                Email_corporativo = dto.Email_corporativo,
                Tel = dto.Tel,
                NSS = dto.NSS,
                RFC = dto.RFC,
                Id_Status = dto.Id_Status,
                Id_Puesto = dto.Id_Puesto,
                Id_Jefe = dto.Id_Jefe,
                Id_Ubicacion = dto.Id_Ubicacion,
                Fotografia = dto.Fotografia
            };

            await _repository.AddAsync(empleado);

            var readDto = new EmpleadoReadDto
            {
                ID = empleado.ID,
                Clave = empleado.Clave,
                Nombres = empleado.Nombres,
                Apellido_Paterno = empleado.Apellido_Paterno,
                Apellido_Materno = empleado.Apellido_Materno,
                Fecha_Nacimiento = empleado.Fecha_Nacimiento,
                Fecha_Inicio = empleado.Fecha_Inicio,
                Fecha_Termino = empleado.Fecha_Termino,
                Email_corporativo = empleado.Email_corporativo,
                Tel = empleado.Tel,
                NSS = empleado.NSS,
                RFC = empleado.RFC,
                Id_Status = empleado.Id_Status,
                Id_Puesto = empleado.Id_Puesto,
                Id_Jefe = empleado.Id_Jefe,
                Id_Ubicacion = empleado.Id_Ubicacion,
                Fotografia = empleado.Fotografia
            };

            return CreatedAtAction(nameof(GetById), new {id = empleado.ID}, readDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, EmpleadoUpdateDto dto)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null) return NotFound();

            empresa.Clave = dto.Clave;
            empresa.Nombres = dto.Nombres;
            empresa.Apellido_Paterno = dto.Apellido_Paterno;
            empresa.Apellido_Materno = dto.Apellido_Materno;
            empresa.Fecha_Nacimiento = dto.Fecha_Nacimiento;
            empresa.Fecha_Inicio = dto.Fecha_Inicio;
            empresa.Fecha_Termino = dto.Fecha_Termino;
            empresa.Email_corporativo = dto.Email_corporativo;
            empresa.Tel = dto.Tel;
            empresa.NSS = dto.NSS;
            empresa.RFC = dto.RFC;
            empresa.Id_Status = dto.Id_Status;
            empresa.Id_Puesto = dto.Id_Puesto;
            empresa.Id_Jefe = dto.Id_Jefe;
            empresa.Id_Ubicacion = dto.Id_Ubicacion;
            empresa.Fotografia = dto.Fotografia;

            await _repository.UpdateAsync(empresa);
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
                Clave = empleado.Clave,
                Nombres = empleado.Nombres,
                Apellido_Paterno = empleado.Apellido_Paterno,
                Apellido_Materno = empleado.Apellido_Materno,
                Fecha_Nacimiento = empleado.Fecha_Nacimiento,
                Fecha_Inicio = empleado.Fecha_Inicio,
                Fecha_Termino = empleado.Fecha_Termino,
                Email_corporativo = empleado.Email_corporativo,
                Tel = empleado.Tel,
                NSS = empleado.NSS,
                RFC = empleado.RFC,
                Id_Status = empleado.Id_Status,
                Id_Puesto = empleado.Id_Puesto,
                Id_Jefe = empleado.Id_Jefe,
                Id_Ubicacion = empleado.Id_Ubicacion,
                Fotografia = empleado.Fotografia
            };

            patchDoc.ApplyTo(dto, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            empleado.Clave = dto.Clave;
            empleado.Nombres = dto.Nombres;
            empleado.Apellido_Paterno = dto.Apellido_Paterno;
            empleado.Apellido_Materno = dto.Apellido_Materno;
            empleado.Fecha_Nacimiento = dto.Fecha_Nacimiento;
            empleado.Fecha_Inicio = dto.Fecha_Inicio;
            empleado.Fecha_Termino = dto.Fecha_Termino;
            empleado.Email_corporativo = dto.Email_corporativo;
            empleado.Tel = dto.Tel;
            empleado.NSS = dto.NSS;
            empleado.RFC = dto.RFC;
            empleado.Id_Status = dto.Id_Status;
            empleado.Id_Puesto = dto.Id_Puesto;
            empleado.Id_Jefe = dto.Id_Jefe;
            empleado.Id_Ubicacion = dto.Id_Ubicacion;
            empleado.Fotografia = dto.Fotografia;

            await _repository.UpdateAsync(empleado);
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