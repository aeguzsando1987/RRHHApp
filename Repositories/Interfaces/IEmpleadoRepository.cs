using RRHH.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RRHH.WebApi.Repositories.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetAllAsync();
        Task<Empleado?> GetByIdAsync(int id); // Return nullable Empleado if not found
        Task AddAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task UpdateEmpleadoTipoAsync(int id, int idTipoEmpleado);
        Task DeleteAsync(int id);
    }
}
