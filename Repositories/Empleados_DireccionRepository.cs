using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Repositories
{
    public class Empleados_DireccionRepository : IEmpleados_DireccionRepository
    {
        private readonly RRHHDbContext _context;

        public Empleados_DireccionRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleados_Direccion>> GetAllAsync()
        {
            return await _context.Empleados_Direcciones
                .Include(e => e.Empleado)
                .ToListAsync();
        }

        public async Task<Empleados_Direccion> GetByIdAsync(int id)
        {
            return await _context.Empleados_Direcciones
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task AddAsync(Empleados_Direccion empleadosDireccion)
        {
            _context.Empleados_Direcciones.Add(empleadosDireccion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empleados_Direccion empleadosDireccion)
        {
            _context.Empleados_Direcciones.Update(empleadosDireccion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var empleadosDireccion = await _context.Empleados_Direcciones.FindAsync(id);
            if (empleadosDireccion != null)
            {
                _context.Empleados_Direcciones.Remove(empleadosDireccion);
                await _context.SaveChangesAsync();
            }
        }
    }
}