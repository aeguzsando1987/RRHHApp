using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;
using RRHH.WebApi.Data;
using RRHH.WebApi.Repositories.Interfaces;

/// <summary>
/// Interfaz para interactuar con la tabla Organizaciones en la base de datos
/// </summary>


namespace RRHH.WebApi.Repositories
{
    public class OrganizacionRepository : IOrganizacionRepository
    {
        // Inicializa el contexto de la base de datos
        private readonly RRHHDbContext _context;

        public OrganizacionRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las organizaciones de la base de datos
        /// </summary>
        /// <returns>Una lista de todas las organizaciones</returns>
        public async Task<IEnumerable<Organizacion>> GetAllAsync()
        {
            // Pide a la base de datos que devuelva todas las organizaciones y espera a que se complete
            return await _context.Organizaciones.ToListAsync(); // 1. Obtiene todas las organizaciones
        }
        /// <summary>
        /// Obtiene una organizacion en particular por su id
        /// </summary>
        /// <param name="id">Id de la organizacion a buscar</param>
        /// <returns>La organizacion encontrada, o null si no existe</returns>
        public async Task<Organizacion> GetByIdAsync(int id)
        {
            // Pide a la base de datos que devuelva la organizacion en particular por su id y espera a que se complete
            return await _context.Organizaciones.FindAsync(id); // 2. Obtiene una organizacion en particular por su id
        }
        /// <summary>
        /// Agrega una nueva organizacion a la base de datos
        /// </summary>
        /// <param name="organizacion">La organizacion a agregar</param>
        public async Task AddAsync(Organizacion organizacion)
        {
            _context.Organizaciones.Add(organizacion);
            await _context.SaveChangesAsync(); // 3. Agrega una nueva organizacion y espera a que se complete
        }

        /// <summary>
        /// Actualiza una organizacion existente en la base de datos
        /// </summary>
        /// <param name="organizacion">La organizacion a actualizar</param>
        public async Task UpdateAsync(Organizacion organizacion)
        {
            _context.Organizaciones.Update(organizacion);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una organizacion existente de la base de datos
        /// </summary>
        /// <param name="id">Id de la organizacion a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            // Busca la organizacion a eliminar
            var organizacion = await _context.Organizaciones.FindAsync(id);
            if (organizacion != null)
            {
                // Si existe, la elimina y espera a que se complete
                _context.Organizaciones.Remove(organizacion);
                await _context.SaveChangesAsync();
            }
        }

    }

}



