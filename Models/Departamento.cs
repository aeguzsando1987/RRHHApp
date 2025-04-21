using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;


namespace RRHH.WebApi.Models {

    public class Departamento {
        [Key]
        public int ID { get; set; }

        [StringLength(20)]
        public string Clave {get; set; } = string.Empty;

        [ForeignKey(nameof(Area))]
        public int Id_Area {get; set; }

        [Required]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [StringLength(100)]
        public string Descripcion {get; set;} = string.Empty;

        public Area Area {get; set; } = null!;
    }


}