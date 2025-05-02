using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace RRHH.WebApi.Models {

    public class Status {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public required string Status_Emp { get; set; }

        [StringLength(100)]
        public string Descripcion_Status { get; set; } = string.Empty;

        public ICollection<Empleado> Empleados {get; set; } = new List<Empleado>();
    }

}