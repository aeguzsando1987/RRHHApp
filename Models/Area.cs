using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models {

    public class Area {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [ForeignKey(nameof(Empresa))]
        public int Id_Empresa { get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();

        public Empresa Empresa { get; set; } = null!;
    }

}