using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IPuestosActividadRepository
    {
        Task<IEnumerable<PuestosActividad>> GetAllAsync();
        Task<PuestosActividad> GetByIdAsync(int id);
        Task AddAsync(PuestosActividad puestosActividad);
        Task UpdateAsync(PuestosActividad puestosActividad);
        Task DeleteAsync(int id);
    }
}
