using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models {


    public class Empleado {
        [Key]
        public int ID { get; set; }
        
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public required string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido_Paterno { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido_Materno { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Termino { get; set; }

        [StringLength(50)]
        public string Email_corporativo { get; set; } = string.Empty;

        [StringLength(20)]
        public string Tel { get; set; } = string.Empty;

        [StringLength(50)]
        public string NSS { get; set; } = string.Empty;

        [StringLength(20)]
        public string RFC { get; set; } = string.Empty;

        [ForeignKey(nameof(Status))]
        public int Id_Status { get; set; }

        [ForeignKey(nameof(Puesto))]
        public int Id_Puesto { get; set; }

        [ForeignKey(nameof(Jefe))]
        public int? Id_Jefe { get; set; }

        [ForeignKey(nameof(Ubicacion))]
        public int Id_Ubicacion { get; set; }

        public byte[] Fotografia { get; set; }


        public Status Status { get; set; }
        public Puesto Puesto { get; set; }
        public Empleado? Jefe { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public User User { get; set; }

    }


}