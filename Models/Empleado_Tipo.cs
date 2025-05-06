using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace RRHH.WebApi.Models
{
    public class Empleado_Tipo
    {
        [Key]
        public int ID{ get; set; }

        [Required]
        [StringLength(50)]
        public required string Titulo { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Prefijo { get; set; } = string.Empty;

        public ICollection<Empleado_Perfil> Perfiles { get; set; }  = new List<Empleado_Perfil>();
    }
}
