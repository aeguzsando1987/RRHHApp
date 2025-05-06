using System;
using System.ComponentModel.DataAnnotations;
using RRHH.WebApi.Models.Enums;


namespace RRHH.WebApi.Models.Dtos.EmpleadoPerfil
{


    public class EmpleadoPerfilCreateDto
    {
        [Required]
        [StringLength(50)]
        public required string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido_Paterno { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido_Materno { get; set; }

        [StringLength(1)]
        public string? Sexo { get; set; }

        [StringLength(20)]
        public string? Edo_Civil { get; set; } 

        public DateTime? Fecha_Nacimiento { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime? Fecha_Termino { get; set; }
        
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Tel { get; set; }

        [StringLength(50)]
        public string? NSS { get; set; }

        [StringLength(20)]
        public string? RFC { get; set; }

        [StringLength(30)]
        public string? Curp { get; set; }

        public byte[]? Fotografia { get; set; }

        [Required]
        public int Id_Tipo_Empleado { get; set; }
    }


}