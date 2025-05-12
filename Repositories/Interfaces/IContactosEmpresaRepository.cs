using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IContactosEmpresaRepository
    {
        Task<IEnumerable<ContactosEmpresa>> GetAllAsync();
        Task<ContactosEmpresa> GetByIdAsync(int id);
        Task AddAsync(ContactosEmpresa contactosEmpresa);
        Task UpdateAsync(ContactosEmpresa contactosEmpresa);
        Task DeleteAsync(int id);
    }
}