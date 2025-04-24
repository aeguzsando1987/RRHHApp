using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Organizacion
{
    public class OrganizacionCreateDto
    {
        [Required]
        [StringLength(20)]
        public string? Clave {get; set;}

        [Required, StringLength(50)]
        public string? Nombre {get; set;}

        [Required]
        public DateTime Fecha_Creacion {get; set;}
    }   
}