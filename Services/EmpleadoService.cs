using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RRHH.WebApi.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EmpleadoService> _logger;

        // Define status IDs for deactivation and reactivation
        private static readonly List<int> DeactivationStatusIds = new List<int> { 2, 3, 4 }; // SUSPENDIDO, BAJA VOLUNTARIA, BAJA INVOLUNTARIA
        private static readonly List<int> ReactivationStatusIds = new List<int> { 1 }; // ACTIVO

        public EmpleadoService(
            IEmpleadoRepository empleadoRepository,
            UserManager<User> userManager,
            ILogger<EmpleadoService> logger)
        {
            _empleadoRepository = empleadoRepository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> UpdateEmpleadoStatusAsync(int idEmpleado, int newStatusId)
        {
            var empleado = await _empleadoRepository.GetByIdAsync(idEmpleado);
            if (empleado == null)
            {
                _logger.LogWarning("Intento de actualizar estatus para empleado no existente ID: {IdEmpleado}", idEmpleado);
                return false;
            }

            empleado.Id_Status = newStatusId;
            try
            {
                await _empleadoRepository.UpdateAsync(empleado);
                _logger.LogInformation("Estatus del empleado ID: {IdEmpleado} actualizado a {NewStatusId} en el repositorio.", idEmpleado, newStatusId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estatus del empleado ID: {IdEmpleado} en el repositorio.", idEmpleado);
                return false; // Indicate failure if repository update throws an exception
            }

            // Buscar usuario asociado al empleado
            // Asumiendo que Id_Empleado en User es nullable y único para un empleado activo.
            // Si la relación es diferente, esta lógica podría necesitar ajuste.
            var user = _userManager.Users.FirstOrDefault(u => u.Id_Empleado == idEmpleado);

            if (user != null)
            {
                bool userWasModified = false;

                // Lógica de Desactivación
                if (DeactivationStatusIds.Contains(newStatusId))
                {
                    if (user.Active)
                    {
                        user.Active = false;
                        userWasModified = true;
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} será desactivado debido al nuevo estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                    else
                    {
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} ya se encuentra inactivo. No se requieren cambios por estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                }
                // Lógica de Reactivación
                else if (ReactivationStatusIds.Contains(newStatusId))
                {
                    if (!user.Active) // Solo reactivar si estaba inactivo
                    {
                        user.Active = true;
                        userWasModified = true;
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} será reactivado debido al nuevo estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                    else
                    {
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} ya se encuentra activo. No se requieren cambios por estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                }

                if (userWasModified)
                {
                    var identityResult = await _userManager.UpdateAsync(user);
                    if (identityResult.Succeeded)
                    {
                        _logger.LogInformation("Estado 'Active' del usuario ID: {UserId} actualizado correctamente a {UserActiveStatus}.", user.Id, user.Active);
                    }
                    else
                    {
                        _logger.LogError("Error al actualizar estado 'Active' del usuario ID: {UserId} para el empleado ID: {IdEmpleado}. Errores: {Errors}", 
                                       user.Id, idEmpleado, string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                        // Considerar si la operación general debe fallar si la actualización del usuario falla.
                        // Por ahora, la actualización del estatus del empleado ya fue exitosa.
                        // Podríamos devolver false aquí si la consistencia es crítica:
                        // return false; 
                    }
                }
            }
            else
            {
                _logger.LogInformation("No se encontró usuario asociado al empleado ID: {IdEmpleado}. No se realizarán cambios en cuentas de usuario.", idEmpleado);
            }

            return true;
        }
    }
}