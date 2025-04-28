using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Jerarquia
{
    public class JerarquiaUpdateDto
    {
        [Required]
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public int Nivel { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; } = string.Empty;

    }

}

