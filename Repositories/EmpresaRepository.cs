using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;


namespace RRHH.WebApi.Repositories
{
    /// <summary>
    /// Interfaz para interactuar con la tabla Empresas en la base de datos
    /// </summary>
    public class EmpresasRepository
    {

        private readonly RRHHDbContext _context;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context">La base de datos</param>
        public EmpresasRepository(RRHHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las empresas de la base de datos
        /// </summary>
        /// <returns>Una lista de todas las empresas</returns>
        /// <remarks>
        /// La lista de empresas incluye la organizacion relacionada y sus areas
        /// </remarks>
        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _context.Empresas
                .Include(e => e.Organizacion) // incluir la organizacion relacionada
                .Include(e => e.Areas) // incluir las areas relacionadas
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una empresa en particular por su id
        /// </summary>
        /// <param name="id">Id de la empresa</param>
        /// <returns>La empresa encontrada, o null si no existe</returns>
        public async Task<Empresa> GetByIdAsync(int id)
        {
            return await _context.Empresas
                .Include(e => e.Organizacion)
                .Include(e => e.Areas)
                .FirstOrDefaultAsync(e => e.ID == id);
        }

        /// <summary>
        /// Agrega una nueva empresa a la base de datos
        /// </summary>
        /// <param name="empresa">La empresa a agregar</param>
        public async Task AddAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza una empresa existente en la base de datos
        /// </summary>
        /// <param name="empresa">La empresa a actualizar</param>
        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina una empresa existente de la base de datos
        /// </summary>
        /// <param name="id">Id de la empresa a eliminar</param>
        public async Task DeleteAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}