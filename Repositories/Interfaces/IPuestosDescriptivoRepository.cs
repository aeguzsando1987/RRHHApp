using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IPuestosDescriptivoRepository
    {
        Task<IEnumerable<PuestosDescriptivo>> GetAllAsync();
        Task<PuestosDescriptivo> GetByIdAsync(int id);
        Task AddAsync(PuestosDescriptivo puestosDescriptivo);
        Task UpdateAsync(PuestosDescriptivo puestosDescriptivo);
        Task DeleteAsync(int id);
    }
}