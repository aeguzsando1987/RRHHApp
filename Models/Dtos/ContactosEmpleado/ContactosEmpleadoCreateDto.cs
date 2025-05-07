using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.ContactosEmpleado
{
    public class ContactosEmpleadoCreateDto
    {
        [Required]
        public int Id_Empleado { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre_Contacto { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Domicilio { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Relacion { get; set; }
    }
}
