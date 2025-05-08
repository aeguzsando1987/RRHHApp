using Microsoft.EntityFrameworkCore;
using RRHH.WebApi.Data;
using RRHH.WebApi.Models;
using RRHH.WebApi.Models.Interfaces;

namespace RRHH.WebApi.Repositories
{
    public class UserRepository
    {
        private readonly RRHHDbContext _context;

        public UserRepository(RRHHDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Empleado)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> ValidateCredentialsAsync(string username, string passwords)
        {
            return await _context.Users
                .Include(u => u.Empleado)
                .FirstOrDefaultAsync(u => u.Username == username && 
                                          u.Password == passwords && 
                                          u.Active);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }


    }
}
