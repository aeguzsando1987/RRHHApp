using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IEmpleados_DireccionRepository
    {
        Task<IEnumerable<Empleados_Direccion>> GetAllAsync();
        Task<Empleados_Direccion> GetByIdAsync(int id);
        Task AddAsync(Empleados_Direccion empleadosDireccion);
        Task UpdateAsync(Empleados_Direccion empleadosDireccion);
        Task DeleteAsync(int id);
    }
}
