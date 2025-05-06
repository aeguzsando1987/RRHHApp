using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using RRHH.WebApi.Models.Enums;

namespace RRHH.WebApi.Models {

        /// <summary>
        /// Entidad que representa a un perfil de un empleado
        /// </summary>
        /// <remarks>
        /// La clave del empleado se genera automaticamente con el formato
        /// <c>Prefix{Id_Empleado}</c>, donde <c>Prefix</c> es un prefijo dependiendo del tipo de empleado.
        /// </remarks>
    public class Empleado_Perfil {
        [Key]
        [ForeignKey(nameof(Empleado))]
        public int Id_Empleado { get; set; }

        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public required string Nombres { get; set; }

        [Required]
        [StringLength(50)]
        public required string Apellido_Paterno { get; set; }


        [Required]
        [StringLength(50)]
        public required string Apellido_Materno { get; set; }

        public DateTime? Fecha_Nacimiento { get; set; }

        [StringLength(1)]
        public string? Sexo { get; set; }

        [StringLength(20)]
        public string? Edo_Civil { get; set; } 

        [Required]
        public DateTime Fecha_Inicio { get; set;}

        public DateTime? Fecha_Termino { get; set;}

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

        [ForeignKey(nameof(Tipo))]
        public int Id_Tipo_Empleado { get; set; }

        public Empleado_Tipo? Tipo { get; set; }


        public Empleado? Empleado { get; set; }

        /// <summary>
        /// Genera una clave unica para el empleado.
        /// </summary>
        /// <remarks>
        /// La clave se genera con el formato <c>Prefix{Id_Empleado}</c>, donde <c>Prefix</c> depende del tipo de empleado.
        /// </remarks>
        public void GenerateClave()
        {
            string prefix = Tipo?.Prefijo ?? "EMP";
            Clave = $"{prefix}{Id_Empleado}";  
        }
      
    }

}