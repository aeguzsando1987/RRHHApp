using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RRHH.WebApi.Models;

namespace RRHH.WebApi.Services
{
    public interface IEmpleadoService
    {
        Task<bool> UpdateEmpleadoStatusAsync(int idEmpleado, int newStatusId);
    }
}