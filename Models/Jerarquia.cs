using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace RRHH.WebApi.Models {

    public class Jerarquia {

        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public required string Titulo {get; set;}

        [Required]
        public required int Nivel {get; set;}

        [StringLength(50)]
        public string Descripcion {get; set; } = string.Empty;

        public ICollection<Puesto> Puestos {get; set; } = new List<Puesto>();

    }

}