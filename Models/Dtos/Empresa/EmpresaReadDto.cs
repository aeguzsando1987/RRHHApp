using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Empresa
{


    public class EmpresaReadDto
    {
        public int Id { get; set; }
        public int Id_Org { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Razon_Social { get; set; } = string.Empty;
        public string? RFC { get; set; }
        public string? Direccion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }


}