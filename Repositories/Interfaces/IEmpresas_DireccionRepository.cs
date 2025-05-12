using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IEmpresas_DireccionRepository
    {
        Task<IEnumerable<Empresas_Direccion>> GetAllAsync();
        Task<Empresas_Direccion> GetByIdAsync(int id);
        Task AddAsync(Empresas_Direccion empresasDireccion);
        Task UpdateAsync(Empresas_Direccion empresasDireccion);
        Task DeleteAsync(int id);
    }
}
