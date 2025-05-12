using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IPuestoRepository
    {
        Task<IEnumerable<Puesto>> GetAllAsync();
        Task<Puesto> GetByIdAsync(int id);
        Task AddAsync(Puesto puesto);
        Task UpdateAsync(Puesto puesto);
        Task DeleteAsync(int id);
    }
}