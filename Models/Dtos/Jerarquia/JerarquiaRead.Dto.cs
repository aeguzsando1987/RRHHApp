using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Jerarquia
{


    public class JerarquiaReadDto
    {
        public int ID { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int Nivel { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        
    }


}