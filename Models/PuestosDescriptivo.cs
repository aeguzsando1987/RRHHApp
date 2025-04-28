using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models {
   
    public class PuestosDescriptivo {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Puesto))]
        public int ID_Puesto { get; set; }
        [StringLength(200)]
        public string Resumen { get; set; } = string.Empty;
        public DateTime Fecha_Actualizacion { get; set; }
        public Puesto? Puesto { get; set; }

        public ICollection<PuestosActividad> PuestosActividad {get; set; } = new List<PuestosActividad>();
       
    }
}