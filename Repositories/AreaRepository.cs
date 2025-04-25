using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{

    public class AreaRepository
    {


        private readonly RRHHDbContext _context;

        public AreaRepository(RRHHDbContext context)
        {
            _context = context;
        }

         public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _context.Areas
                .Include(a => a.Empresa) // incluir las empresas relacionadas
                .Include(a => a.Departamentos) // incluir los departamentos relacionadas
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una area en particular por su id
        /// </summary>
        /// <param name="id">Id de la empresa</param>
        /// <returns>La empresa encontrada, o null si no existe</returns>
        public async Task<Area> GetByIdAsync(int id)
        {
            return await _context.Areas
                .Include(a => a.Empresa)
                .Include(a => a.Departamentos)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        /// <summary>
        /// Agrega una nueva area a la base de datos
        /// </summary>
        /// <param name="area">La empresa a agregar</param>
        public async Task AddSync(Area area)
        {
            _context.Areas.Add(area);
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Actualiza una area existente en la base de datos
        /// </summary>
        /// <param name="area">La empresa a actualizar</param>
        public async Task UpdateAsync(Area area)
        {
            _context.Areas.Update(area);
            await _context.SaveChangesAsync();
        }

         /// <summary>
        /// Elimina una empresa existente de la base de datos
        /// </summary>
        /// <param name="id">Id de la empresa a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area != null)
            {
                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
            }
        }

        
    }

}