using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Puesto
{


    public class PuestoCreateDto
    {

  

        [Required]
        [StringLength(20)]

        public string Clave {get; set;} = string.Empty;

        [Required]
        [StringLength(50)]
        public string Titulo {get; set;} = string.Empty;

        [Required]
        [StringLength(100)]
        public string Descripcion {get; set;} = string.Empty;

        [Required]
        public int Id_Departamento {get; set;}

        [Required]
        public int Id_Jerarquia {get; set;}

    }


}