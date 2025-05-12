using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllAsync();
        Task<Status> GetByIdAsync(int id);
        Task AddAsync(Status status);
        Task UpdateAsync(Status status);
        Task DeleteAsync(int id);
    }
}
