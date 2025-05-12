using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IUbicacionRepository
    {
        Task<IEnumerable<Ubicacion>> GetAllAsync();
        Task<Ubicacion> GetByIdAsync(int id);
        Task AddAsync(Ubicacion ubicacion);
        Task UpdateAsync(Ubicacion ubicacion);
        Task DeleteAsync(int id);
    }
}
