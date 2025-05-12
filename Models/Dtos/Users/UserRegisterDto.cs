using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Users
{
    /// <summary>
    /// DTO para el registro de nuevos usuarios.
    /// Contiene la información necesaria para crear una nueva cuenta de usuario.
    /// </summary>
    public class UserRegisterDto
    {
        /// <summary>
        /// Nombre de usuario para la nueva cuenta.
        /// Debe ser único en el sistema.
        /// </summary>
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "El nombre de usuario debe tener al menos 8 caracteres.")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña para la nueva cuenta.
        /// Debe cumplir con los requisitos de complejidad definidos.
        /// </summary>
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        // Consider adding regex for password complexity if desired, e.g.:
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,100}$", ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del empleado asociado a esta cuenta de usuario.
        /// Este campo vincula la cuenta de usuario con un registro de empleado existente.
        /// </summary>
        [Required(ErrorMessage = "El ID del empleado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del empleado debe ser un número positivo.")]
        public int Id_Empleado { get; set; }
    }
}
