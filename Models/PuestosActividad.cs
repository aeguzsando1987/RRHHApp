using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models {
   
    public class PuestosActividad {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Puesto))]
        public int ID_PuestoDescriptivo { get; set; }
        [StringLength(50)]
        public string Titulo { get; set; } = string.Empty;
        [StringLength(100)]
        public string Resumen { get; set; } = string.Empty;
        public DateTime Fecha_Actualizacion { get; set; }
        public PuestosDescriptivo? PuestosDescriptivo { get; set; }
       
    }
}