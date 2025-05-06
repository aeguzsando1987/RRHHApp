using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace RRHH.WebApi.Repositories
{
    public class UbicacionRepository
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

        public async Task AddSync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Add(ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSync(Ubicacion ubicacion)
        {
            _context.Ubicaciones.Update(ubicacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSync(int id)
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