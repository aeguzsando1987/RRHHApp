using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models {

    public class Organizacion {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }
        public DateTime Fecha_Creacion { get; set; }

        public ICollection<Empresa> Empresas { get; set; } = new List<Empresa>();
    }

}