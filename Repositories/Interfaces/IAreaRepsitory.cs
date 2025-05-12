using RRHH.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IAreaRepository
    {
        Task<IEnumerable<Area>> GetAllAsync();
        Task<Area> GetByIdAsync(int id);
        Task AddAsync(Area area);
        Task UpdateAsync(Area area);
        Task DeleteAsync(int id);
    }
}