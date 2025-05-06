using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Enums;

namespace RRHH.WebApi.Repositories
{
    public class EmpleadoRepository
    {
        private readonly RRHHDbContext _context;

        public EmpleadoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .Include(e => e.Status)
                .Include(e => e.Perfil)
                .ToListAsync();
        }

        public async Task<Empleado> GetByIdAsync(int id)
        {
            return await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .Include(e => e.Status)
                .Include(e => e.Perfil)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task AddAsync(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            // Generar Clave de Empleado despues de guardar (Ya se requiere ID)
            // Si el empleado tiene perfil, generar clave
            if (empleado.Perfil != null)
            {
                // Generar Clave de Empleado después de guardar (Ya se requiere ID)
                // Aquí llamamos al método GenerateClave que crea una clave única basada en el tipo de empleado
                empleado.Perfil.GenerateClave();
                // Guardamos los cambios en la base de datos utilizando Entity Framework
                // SaveChangesAsync persiste todas las modificaciones pendientes en la base de datos
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            // Generar Clave de Empleado despues de guardar (Ya se requiere ID)
            if (empleado.Perfil != null)
            {
                empleado.Perfil.GenerateClave();
            }

            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateEmpleadoTipoAsync(int id, int idTipoEmpleado)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Perfil)
                .FirstOrDefaultAsync(e => e.ID == id);

            if (empleado != null && empleado.Perfil != null)
            {
                empleado.Perfil.Id_Tipo_Empleado = idTipoEmpleado;
                empleado.Perfil.GenerateClave();
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
            }
        }

    }
}