using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{

    /// <summary>
    /// Repositorio de Puestos Actividad.
    /// Esta clase se encarga de interactuar con la base de datos para obtener, agregar, actualizar y eliminar
    /// actividades de puestos descriptivos.
    /// </summary>
    public class PuestosActividadRepository
    {

        private readonly RRHHDbContext _context;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="context">La base de datos</param>
        public PuestosActividadRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las actividades de todos los puestos descriptivos de la base de datos.
        /// </summary>
        /// <returns>Una lista de todas las actividades de todos los puestos descriptivos</returns>
        public async Task<IEnumerable<PuestosActividad>> GetAllAsync()
        {
            return await _context.PuestosActividad
                .Include(pa => pa.PuestosDescriptivo)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una actividad de un puesto descriptivo en particular por su id.
        /// </summary>
        /// <param name="id">Id de la actividad del puesto descriptivo</param>
        /// <returns>La actividad encontrada, o null si no existe</returns>
        public async Task<PuestosActividad> GetByIDAsync(int id)
        {

            return await _context.PuestosActividad
                .Include(pd => pd.PuestosDescriptivo)
                .FirstOrDefaultAsync(pd => pd.ID == id);
        }

        /// <summary>
        /// Agrega una nueva actividad a un puesto descriptivo.
        /// </summary>
        /// <param name="puestosActividad">La actividad a agregar</param>
        public async Task AddAsync(PuestosActividad puestosActividad)
        {
            _context.PuestosActividad.Add(puestosActividad);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una actividad de un puesto descriptivo.
        /// </summary>
        /// <param name="puestosActividad">La actividad a actualizar</param>
        public async Task UpdateAsync(PuestosActividad puestosActividad)
        {
            _context.PuestosActividad.Update(puestosActividad);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una actividad de un puesto descriptivo.
        /// </summary>
        /// <param name="id">Id de la actividad del puesto descriptivo</param>
        public async Task DeleteAsync(int id)
        {
            var puestosActividad = await _context.PuestosActividad.FindAsync(id);
            if (puestosActividad != null)
            {
                _context.PuestosActividad.Remove(puestosActividad);
                await _context.SaveChangesAsync();
            }
        }



    }


}