using System.Collections.Generic;
using System.Threading.Tasks;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Repositories.Interfaces;

public interface IEmpleadoTipoRepository
{
    Task<IEnumerable<Empleado_Tipo>> GetAllAsync();
    Task<Empleado_Tipo> GetByIdAsync(int id);
    Task AddAsync(Empleado_Tipo empleadoTipo);
    Task UpdateAsync(Empleado_Tipo empleadoTipo);
    Task DeleteAsync(int id);
}
