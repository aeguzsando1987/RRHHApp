using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Users
{
    /// <summary>
    /// DTO para el inicio de sesión de usuarios.
    /// Contiene las credenciales necesarias para autenticar a un usuario.
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Nombre de usuario para el inicio de sesión.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña para el inicio de sesión.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}
