using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{

    /// <summary>
    /// Repositorio de Puestos Descriptivos.
    /// Esta clase se encarga de interactuar con la base de datos para obtener, agregar, actualizar y eliminar
    /// puestos descriptivos.
    /// </summary>
    public class PuestosDescriptivoRepository
    {
        private readonly RRHHDbContext _context;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="context">La base de datos</param>
        public PuestosDescriptivoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los puestos descriptivos de la base de datos.
        /// </summary>
        /// <returns>Una lista de todos los puestos descriptivos</returns>
        public async Task<IEnumerable<PuestosDescriptivo>> GetAllAsync()
        {
            return await _context.PuestosDescriptivo
                .Include(pd => pd.Puesto) // incluir las areas relacionadas
                .Include(pd => pd.PuestosActividad) // incluir los Empleados relacionados
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un puesto descriptivo en particular por su id.
        /// </summary>
        /// <param name="id">Id del puesto descriptivo</param>
        /// <returns>El puesto descriptivo encontrada, o null si no existe</returns>
        public async Task<PuestosDescriptivo> GetByIdAsync(int id)
        {
            return await _context.PuestosDescriptivo
                .Include(pd => pd.Puesto)
                .Include(pd => pd.PuestosActividad)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        /// <summary>
        /// Agrega un nuevo puesto descriptivo a la base de datos.
        /// </summary>
        /// <param name="puestosDescriptivo">El puesto descriptivo a agregar</param>
        public async Task AddAsync(PuestosDescriptivo puestosDescriptivo)
        {
            _context.PuestosDescriptivo.Add(puestosDescriptivo); // Agrega el nuevo elemento a la tabla
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        /// <summary>
        /// Actualiza un puesto descriptivo existente en la base de datos.
        /// </summary>
        /// <param name="puestosDescriptivo">El puesto descriptivo a actualizar</param>
        public async Task UpdateAsync(PuestosDescriptivo puestosDescriptivo)
        {
            _context.PuestosDescriptivo.Update(puestosDescriptivo); // Actualiza el elemento en la tabla
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        /// <summary>
        /// Elimina un puesto descriptivo existente de la base de datos.
        /// </summary>
        /// <param name="id">Id del puesto descriptivo a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var puesto = await _context.PuestosDescriptivo.FindAsync(id);
            if (puesto != null)
            {
                _context.PuestosDescriptivo.Remove(puesto); // Elimina el elemento de la tabla
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}