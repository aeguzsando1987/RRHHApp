using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Empleado
{
    public class EmpleadoCreateDto
    {
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Nombres { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string Apellido_Paterno { get; set;} = string.Empty;
        [Required]
        [StringLength(50)]
        public string Apellido_Materno { get; set;} = string.Empty;

        [Required]
        public DateTime Fecha_Nacimiento { get; set;}
        [Required]
        public DateTime Fecha_Inicio { get; set;}
        public DateTime Fecha_Termino { get; set;}
        [StringLength(50)]
        public string Email_corporativo { get; set; } = string.Empty;
        [StringLength(20)]
        public string Tel { get; set; } = string.Empty;
        [StringLength(50)]
        public string NSS { get; set; } = string.Empty;
        [StringLength(20)]
        public string RFC { get; set; } = string.Empty;
        [Required]
        public int Id_Status { get; set; }
        [Required]
        public int Id_Puesto { get; set; }
        [Required]
        public int Id_Jefe { get; set; }
        [Required]
        public int Id_Ubicacion { get; set; }
        public byte[]? Fotografia { get; set; }
        
        


    }
}