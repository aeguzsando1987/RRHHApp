using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{

    public class PuestoRepository
    {
        private readonly RRHHDbContext _context;

        public PuestoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los puestos de la base de datos
        /// </summary>
        /// <returns>Una lista de todos los puestos</returns>
        public async Task<IEnumerable<Puesto>> GetAllAsync()
        {
            return await _context.Puestos
                .Include(p => p.Departamento) // incluir las areas relacionadas
                .Include(p => p.Empleados) // incluir los Empleados relacionados
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un puesto en particular por su id
        /// </summary>
        /// <param name="id">Id del puesto</param>
        /// <returns>El puesto encontrada, o null si no existe</returns>
        public async Task<Puesto> GetByIdAsync(int id)
        {
            return await _context.Puestos
                .Include(p => p.Departamento)
                .Include(p => p.Empleados)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        /// <summary>
        /// Agrega un nuevo puesto a la base de datos
        /// </summary>
        /// <param name="puesto">El puesto a agregar</param>
        public async Task AddSync(Puesto puesto)
        {
            _context.Puestos.Add(puesto); // Agrega el nuevo elemento a la tabla
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        /// <summary>
        /// Actualiza un puesto existente en la base de datos
        /// </summary>
        /// <param name="puesto">El puesto a actualizar</param>
        public async Task UpdateAsync(Puesto puesto)
        {
            _context.Puestos.Update(puesto); // Actualiza el elemento en la tabla
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        /// <summary>
        /// Elimina un puesto existente de la base de datos
        /// </summary>
        /// <param name="id">Id del puesto a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var puesto = await _context.Puestos.FindAsync(id);
            if (puesto != null)
            {
                _context.Puestos.Remove(puesto); // Elimina el elemento de la tabla
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }

}