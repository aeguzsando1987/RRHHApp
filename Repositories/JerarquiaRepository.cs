using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Repositories
{

    public class JerarquiaRepository : IJerarquiaRepository
    {
        private readonly RRHHDbContext _context;

        public JerarquiaRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las jerarquias de la base de datos
        /// </summary>
        /// <returns>Una lista de todas las jerarquias</returns>
        public async Task<IEnumerable<Jerarquia>> GetAllAsync()
        {
            return await _context.Jerarquias
                .Include(j => j.Puestos)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una jerarquia en particular por su id
        /// </summary>
        /// <param name="id">Id de la jerarquia</param>
        /// <returns>La jerarquia encontrada, o null si no existe</returns>
        public async Task<Jerarquia> GetByIdAsync(int id)
        {
            return await _context.Jerarquias
                .Include(j => j.Puestos)
                .FirstOrDefaultAsync(j => j.ID == id);
        }

        /// <summary>
        /// Agrega una nueva jerarquia a la base de datos
        /// </summary>
        /// <param name="jerarquia">La jerarquia a agregar</param>
        public async Task AddAsync(Jerarquia jerarquia)
        {
            _context.Jerarquias.Add(jerarquia);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una jerarquia existente en la base de datos
        /// </summary>
        /// <param name="jerarquia">La jerarquia a actualizar</param>
        public async Task UpdateAsync(Jerarquia jerarquia)
        {
            _context.Jerarquias.Update(jerarquia);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una jerarquia existente de la base de datos
        /// </summary>
        /// <param name="id">Id de la jerarquia a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var jerarquia = await _context.Jerarquias.FindAsync(id);
            if (jerarquia != null)
            {
                _context.Jerarquias.Remove(jerarquia);
                await _context.SaveChangesAsync();
            }
        }
    }

}