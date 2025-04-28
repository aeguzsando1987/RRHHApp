using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Jerarquia
{
    public class JerarquiaCreateDto
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public int Nivel { get; set; }

        [StringLength(50)]
        public string Descripcion { get; set; } = string.Empty;
    }
}