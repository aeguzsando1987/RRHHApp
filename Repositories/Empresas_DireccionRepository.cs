using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{
    public class Empresas_DireccionRepository
    {
        private readonly RRHHDbContext _context;

        public Empresas_DireccionRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresas_Direccion>> GetAllAsync()
        {
            return await _context.Empresas_Direcciones
                .Include(e => e.Empresa)
                .ToListAsync();
        }

        public async Task<Empresas_Direccion> GetByIdAsync(int id)
        {
            return await _context.Empresas_Direcciones
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task AddAsync(Empresas_Direccion empresasDireccion)
        {
            _context.Empresas_Direcciones.Add(empresasDireccion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Empresas_Direccion empresasDireccion)
        {
            _context.Empresas_Direcciones.Update(empresasDireccion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var empresasDireccion = await _context.Empresas_Direcciones
                .FirstOrDefaultAsync(e => e.ID == id);
            if (empresasDireccion != null)
            {
                _context.Empresas_Direcciones.Remove(empresasDireccion);
                await _context.SaveChangesAsync();
            }
        }
    }
}