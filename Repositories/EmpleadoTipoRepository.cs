using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Repositories;

namespace RRHH.WebApi.Repositories
{
    public class EmpleadoTipoRepository
    {
        private readonly RRHHDbContext _context;

        public EmpleadoTipoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado_Tipo>> GetAllAsync()
        {
            return await _context.Empleados_Tipo.ToListAsync();
        }

        public async Task<Empleado_Tipo> GetByIdAsync(int id)
        {
            return await _context.Empleados_Tipo.FindAsync(id);
        }

        public async Task AddAsync(Empleado_Tipo empleadoTipo)
        {
            _context.Empleados_Tipo.Add(empleadoTipo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empleado_Tipo empleadoTipo)
        {
            _context.Empleados_Tipo.Update(empleadoTipo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var empleadoTipo = await _context.Empleados_Tipo.FindAsync(id);
            if (empleadoTipo != null)
            {
                _context.Empleados_Tipo.Remove(empleadoTipo);
                await _context.SaveChangesAsync();
            }
        }
    }
}