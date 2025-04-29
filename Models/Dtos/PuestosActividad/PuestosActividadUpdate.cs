using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RRHH.WebApi.Models.Dtos.PuestosActividad
{
    public class PuestosActividadUpdateDto
    {

        [Required]
        public int ID_PuestosDescriptivo { get; set; }
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; } = string.Empty;
        [StringLength(100)]
        public string Resumen { get; set;} = string.Empty;

        public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now; 

        
    }
}