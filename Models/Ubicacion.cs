using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RRHH.WebApi.Models {

    public class Ubicacion {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        public string? Clave { get; set; }
        [StringLength(200)]
        public string? Ubicacion_Referencial {get; set; }
        [ForeignKey(nameof(Empresa))]
        public int Id_Empresa { get; set; }
        public Empresa Empresa {get; set; } = null!;
        public ICollection<Empleado> Empleados {get; set; } = new List<Empleado>();
    }

}

