using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Puesto
{


    public class PuestoReadDto
    {
        public int ID {get; set;}
        public string Clave {get; set;} = string.Empty;
        public int Id_Departamento {get; set;}
        public int Id_Jerarquia {get; set;}
        public string Titulo {get; set;} = string.Empty;
        public string Descripcion {get; set;} = string.Empty;

    }


}