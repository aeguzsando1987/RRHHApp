using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Departamento
{


    public class DepartamentoReadDto 
    {
        public int ID {get; set;}
        public string Clave {get; set;} = string.Empty;
        public int Id_Area {get; set;}
        public string Nombre {get; set;} = string.Empty;
        public string Descripcion {get; set;} = string.Empty;

    }


}