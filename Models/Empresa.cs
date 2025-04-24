using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models {
   
    public class Empresa {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Organizacion))]
        public int Id_Org { get; set; }
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public required string Razon_Social { get; set; }
        [StringLength(20)]
        public string RFC { get; set; } = string.Empty;
        [StringLength(200)]
        public string Direccion {get; set; } = string.Empty;
        public DateTime Fecha_Creacion { get; set; }

        // Navigation properties
        public  Organizacion? Organizacion { get; set; }
        public ICollection<Area>? Areas {get; set;} = new List<Area>(); 

    }
}