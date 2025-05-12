using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IContactosEmpleadoRepository
    {
        Task<IEnumerable<ContactosEmpleado>> GetAllAsync();
        Task<ContactosEmpleado> GetByIdAsync(int id);
        Task AddAsync(ContactosEmpleado contactosEmpleado);
        Task UpdateAsync(ContactosEmpleado contactosEmpleado);
        Task DeleteAsync(int id);
    }
}