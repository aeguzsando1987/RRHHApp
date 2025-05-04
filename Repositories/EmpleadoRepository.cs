using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

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
            return await _context.Empleados.ToListAsync();
        }

        public async Task<Empleado> GetByIdAsync(int id)
        {
            return await _context.Empleados
                .Include(e => e.Puesto)
                .Include(e => e.Jefe)
                .Include(e => e.Status)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task AddAsync(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
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