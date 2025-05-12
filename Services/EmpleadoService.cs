using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RRHH.WebApi.Models;
using RRHH.WebApi.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RRHH.WebApi.Services
{
    /// <summary>
    /// Servicio que maneja operaciones relacionadas con empleados, incluyendo la gestión de su estado.
    /// </summary>
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<EmpleadoService> _logger;

        // Define status IDs para desactivación y reactivación de empleados
        private static readonly List<int> DeactivationStatusIds = new List<int> { 2, 3, 4 }; // SUSPENDIDO, BAJA VOLUNTARIA, BAJA INVOLUNTARIA
        private static readonly List<int> ReactivationStatusIds = new List<int> { 1 }; // ACTIVO

        /// <summary>
        /// Constructor del servicio de empleados.
        /// </summary>
        /// <param name="empleadoRepository">Repositorio para acceder a datos de empleados</param>
        /// <param name="userManager">Gestor de usuarios para manejar cuentas asociadas</param>
        /// <param name="logger">Servicio de registro para documentar operaciones</param>
        public EmpleadoService(
            IEmpleadoRepository empleadoRepository,
            UserManager<User> userManager,
            ILogger<EmpleadoService> logger)
        {
            _empleadoRepository = empleadoRepository;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Actualiza el estatus de un empleado y gestiona los cambios correspondientes en su cuenta de usuario.
        /// </summary>
        /// <param name="idEmpleado">Identificador del empleado a actualizar</param>
        /// <param name="newStatusId">Nuevo identificador de estatus para asignar al empleado</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario</returns>
        public async Task<bool> UpdateEmpleadoStatusAsync(int idEmpleado, int newStatusId)
        {
            // Obtiene el empleado desde el repositorio
            var empleado = await _empleadoRepository.GetByIdAsync(idEmpleado);
            if (empleado == null)
            {
                // Registra advertencia si el empleado no existe
                _logger.LogWarning("Intento de actualizar estatus para empleado no existente ID: {IdEmpleado}", idEmpleado);
                return false;
            }

            // Actualiza el estatus del empleado
            empleado.Id_Status = newStatusId;
            try
            {
                // Persiste los cambios en la base de datos
                await _empleadoRepository.UpdateAsync(empleado);
                _logger.LogInformation("Estatus del empleado ID: {IdEmpleado} actualizado a {NewStatusId} en el repositorio.", idEmpleado, newStatusId);
            }
            catch (Exception ex)
            {
                // Registra error si falla la actualización
                _logger.LogError(ex, "Error al actualizar el estatus del empleado ID: {IdEmpleado} en el repositorio.", idEmpleado);
                return false; // Indica fallo si el repositorio lanza una excepción
            }

            // Busca usuario asociado al empleado
            var user = _userManager.Users.FirstOrDefault(u => u.Id_Empleado == idEmpleado);

            if (user != null)
            {
                // Bandera para controlar si el usuario fue modificado
                bool userWasModified = false;

                // Lógica de Desactivación: si el nuevo estatus corresponde a un estado inactivo
                if (DeactivationStatusIds.Contains(newStatusId))
                {
                    if (user.Active)
                    {
                        // Desactiva el usuario si estaba activo
                        user.Active = false;
                        userWasModified = true;
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} será desactivado debido al nuevo estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                    else
                    {
                        // Registra que no se requieren cambios si ya estaba inactivo
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} ya se encuentra inactivo. No se requieren cambios por estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                }
                // Lógica de Reactivación: si el nuevo estatus corresponde a un estado activo
                else if (ReactivationStatusIds.Contains(newStatusId))
                {
                    if (!user.Active)
                    {
                        // Reactiva el usuario si estaba inactivo
                        user.Active = true;
                        userWasModified = true;
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} será reactivado debido al nuevo estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                    else
                    {
                        // Registra que no se requieren cambios si ya estaba activo
                        _logger.LogInformation("Usuario ID: {UserId} asociado al empleado ID: {IdEmpleado} ya se encuentra activo. No se requieren cambios por estatus {NewStatusId}.", user.Id, idEmpleado, newStatusId);
                    }
                }

                // Si el usuario fue modificado, actualizar en la base de datos
                if (userWasModified)
                {
                    // Persiste los cambios del usuario
                    var identityResult = await _userManager.UpdateAsync(user);
                    if (identityResult.Succeeded)
                    {
                        // Registra éxito en la actualización
                        _logger.LogInformation("Estado 'Active' del usuario ID: {UserId} actualizado correctamente a {UserActiveStatus}.", user.Id, user.Active);
                    }
                    else
                    {
                        // Registra errores si la actualización falló
                        _logger.LogError("Error al actualizar estado 'Active' del usuario ID: {UserId} para el empleado ID: {IdEmpleado}. Errores: {Errors}", 
                                       user.Id, idEmpleado, string.Join(", ", identityResult.Errors.Select(e => e.Description)));
                        // Nota: Aquí se podría devolver false si la consistencia entre empleado y usuario es crítica
                    }
                }
            }
            else
            {
                // Registra que no hay usuario asociado al empleado
                _logger.LogInformation("No se encontró usuario asociado al empleado ID: {IdEmpleado}. No se realizarán cambios en cuentas de usuario.", idEmpleado);
            }

            // La operación se considera exitosa
            return true;
        }
    }
}