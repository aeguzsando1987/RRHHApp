using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetAllAsync();
        Task<Departamento> GetByIdAsync(int id);
        Task AddAsync(Departamento departamento);
        Task UpdateAsync(Departamento departamento);
        Task DeleteAsync(int id);
    }
}