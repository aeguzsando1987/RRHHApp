using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models {


    public class Status {
        [Key]
        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        public required string Estado { get; set; }

        public ICollection<Empleado> Empleados {get; set; } = new List<Empleado>();
    }


}