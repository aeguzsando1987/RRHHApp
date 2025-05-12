using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Repositories
{
    public class UbicacionRepository : IUbicacionRepository
    {
        private readonly RRHHDbContext _context;

        public UbicacionRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ubicacion>> GetAllAsync()
        {
            return await _context.Ubicaciones.ToListAsync();
        }

        public async Task<Ubicacion> GetByIdAsync(int id)
        {
            return await _context.Ubicaciones.FindAsync(id);
        }

        public async Task AddAsync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Update(ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var ubicacion = await _context.Ubicaciones.FindAsync(id);
            if (ubicacion != null)
            {
                _context.Ubicaciones.Remove(ubicacion);
                await _context.SaveChangesAsync();
            }
        }
    }
}