using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Empresa
{
    public class EmpresaUpdateDto
    {
        [Required]
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Razon_Social { get; set; } = string.Empty;

        [StringLength(20)]
        public string? RFC { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        [Required]
        public DateTime Fecha_Creacion { get; set; }

    }

}

