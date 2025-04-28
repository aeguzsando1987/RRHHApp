using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models 
{
    public class ContactosEmpleado
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Empleado))]
        public required int Id_Empleado { get; set; }

        [StringLength(50)]
        public string Nombre_Contacto { get; set; } = string.Empty;

        [StringLength(50)]
        public string Domicilio { get; set; } = string.Empty;

        [StringLength(50)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Relacion { get; set; } = string.Empty;

        public Empleado Empleado { get; set; } = null!;
    }
}