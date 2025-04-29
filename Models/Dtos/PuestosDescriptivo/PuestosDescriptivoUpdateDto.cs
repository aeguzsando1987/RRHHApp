using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.PuestosDescriptivo
{
    public class PuestosDescriptivoUpdateDto
    
    {
        [Required]
        public int Id_Puesto { get; set; }
        [Required]
        [StringLength(200)]
        public string Resumen {get; set;} = string.Empty;
        [Required]
        public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;

        
    }

}

