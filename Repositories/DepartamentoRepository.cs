using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{

    public class DepartamentoRepository
    {
        private readonly RRHHDbContext _context;

        public DepartamentoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los departamentos de la base de datos
        /// </summary>
        /// <returns>Una lista de todos los departamentos</returns>
        public async Task<IEnumerable<Departamento>> GetAllAsync()
        {
            return await _context.Departamentos
                .Include(d => d.Area) // incluir las areas relacionadas
                .Include(d => d.Puestos) // incluir los Puestos relacionados
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un departamento en particular por su id
        /// </summary>
        /// <param name="id">Id del departamento</param>
        /// <returns>El departamento encontrada, o null si no existe</returns>
        public async Task<Departamento> GetByIdAsync(int id)
        {
            return await _context.Departamentos
                .Include(d => d.Area)
                .Include(d => d.Puestos)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        /// <summary>
        /// Agrega un nuevo departamento a la base de datos
        /// </summary>
        /// <param name="departamento">El departamento a agregar</param>
        public async Task AddSync(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un departamento existente en la base de datos
        /// </summary>
        /// <param name="departamento">El departamento a actualizar</param>
        public async Task UpdateAsync(Departamento departamento)
        {
            _context.Departamentos.Update(departamento);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un departamento existente de la base de datos
        /// </summary>
        /// <param name="id">Id del departamento a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento != null)
            {
                _context.Departamentos.Remove(departamento);
                await _context.SaveChangesAsync();
            }
        }
    }

}