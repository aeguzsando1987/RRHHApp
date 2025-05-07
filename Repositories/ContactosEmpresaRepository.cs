using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories
{
    public class ContactosEmpresaRepository
    {
        private readonly RRHHDbContext _context;

        public ContactosEmpresaRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactosEmpresa>> GetAllAsync()
        {
            return await _context.ContactosEmpresas
                .Include(c => c.Empresa)
                .ToListAsync();
        }

        public async Task<ContactosEmpresa> GetByIdAsync(int id)
        {
            return await _context.ContactosEmpresas
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task AddAsync(ContactosEmpresa contactosEmpresa)
        {
            _context.ContactosEmpresas.Add(contactosEmpresa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactosEmpresa contactosEmpresa)
        {
            _context.ContactosEmpresas.Update(contactosEmpresa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contactosEmpresa = await _context.ContactosEmpresas.FindAsync(id);
            if (contactosEmpresa != null)
            {
                _context.ContactosEmpresas.Remove(contactosEmpresa);
                await _context.SaveChangesAsync();
            }
        }
    }
}
