using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models {


    public class Puesto {
        [Key]
        public int ID { get; set; }
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public required string Titulo { get; set; }
        [StringLength(200)]
        public string Descripcion {get; set; } = string.Empty;

        [ForeignKey(nameof(Departamento))]
        public int Id_Departamento { get; set; }

        [ForeignKey(nameof(Jerarquia))]
        public int Id_Jerarquia { get; set; }

        public Departamento Departamento {get; set; } = null!;
        public Jerarquia Jerarquia {get; set; } = null!;
        public ICollection<Empleado> Empleados {get; set; } = new List<Empleado>();

        public ICollection<PuestosDescriptivo> PuestosDescriptivo {get; set; } = new List<PuestosDescriptivo>();
    }
}