using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories.Interfaces;

namespace RRHH.WebApi.Repositories
{
    public class ContactosEmpleadoRepository : IContactosEmpleadoRepository
    {
        private readonly RRHHDbContext _context;

        public ContactosEmpleadoRepository(RRHHDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactosEmpleado>> GetAllAsync()
        {
            return await _context.ContactosEmpleados
                .Include(c => c.Empleado)
                .ToListAsync();
        }

        public async Task<ContactosEmpleado> GetByIdAsync(int id)
        {
            return await _context.ContactosEmpleados
                .Include(c => c.Empleado)
                .FirstOrDefaultAsync(c => c.ID == id);
        }

        public async Task AddAsync(ContactosEmpleado contactosEmpleado)
        {
            _context.ContactosEmpleados.Add(contactosEmpleado);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactosEmpleado contactosEmpleado)
        {
            _context.ContactosEmpleados.Update(contactosEmpleado);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contactosEmpleado = await _context.ContactosEmpleados.FindAsync(id);
            if (contactosEmpleado != null)
            {
                _context.ContactosEmpleados.Remove(contactosEmpleado);
                await _context.SaveChangesAsync();
            }
        }
    }
}