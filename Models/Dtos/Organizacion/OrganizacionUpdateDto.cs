using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Organizacion
{


    public class OrganizacionUpdateDto
    {
        [Required]
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime Fecha_Creacion { get; set; }
    }


}
