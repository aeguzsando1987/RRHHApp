using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IJerarquiaRepository
    {
        Task<IEnumerable<Jerarquia>> GetAllAsync();
        Task<Jerarquia?> GetByIdAsync(int id);
        Task AddAsync(Jerarquia jerarquia);
        Task UpdateAsync(Jerarquia jerarquia);
        Task DeleteAsync(int id);
    }
}
