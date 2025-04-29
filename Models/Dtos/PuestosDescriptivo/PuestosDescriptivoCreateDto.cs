using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.PuestosDescriptivo
{
    public class PuestosDescriptivoCreateDto
    {
        [Required]
        public int ID_Puesto { get; set; }

        [Required]
        [StringLength(200)]  
        public string Resumen {get; set;} = string.Empty;

        public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;

    }   
}