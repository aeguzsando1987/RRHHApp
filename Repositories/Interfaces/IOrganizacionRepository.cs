using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;
using RRHH.WebApi.Data;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IOrganizacionRepository
    {
        Task<IEnumerable<Organizacion>> GetAllAsync();
        Task<Organizacion> GetByIdAsync(int id);
        Task AddAsync(Organizacion organizacion);
        Task UpdateAsync(Organizacion organizacion);
        Task DeleteAsync(int id);
    }
}
