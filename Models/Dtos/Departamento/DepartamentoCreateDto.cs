using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Departamento
{


    public class DepartamentoCreateDto
    {

        [Required]
        public int Id_Area {get; set;}

        [Required]
        [StringLength(20)]

        public string Clave {get; set;} = string.Empty;

        [Required]
        [StringLength(50)]
        public string Nombre {get; set;} = string.Empty;

        [Required]
        [StringLength(100)]
        public string Descripcion {get; set;} = string.Empty;

    }


}