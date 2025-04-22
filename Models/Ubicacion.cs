using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RRHH.WebApi.Models {

    public class Ubicacion {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string Clave { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }
        [StringLength(200)]
        public string Descripcion {get; set; } = string.Empty;

        public ICollection<Empleado> Empleados {get; set; } = new List<Empleado>();
    }

}

