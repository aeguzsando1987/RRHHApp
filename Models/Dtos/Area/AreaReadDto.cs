using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Area
{


    public class AreaReadDto 
    {

        public int ID {get; set;}
        public int Id_Empresa {get; set;}

        public string Clave {get; set;} = string.Empty;

        public string Nombre {get; set;} = string.Empty;

        public string Descripcion {get; set;} = string.Empty;

    }


}