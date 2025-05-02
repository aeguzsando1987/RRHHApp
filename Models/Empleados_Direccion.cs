using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models
{
    public class Empleados_Direccion
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Empleado))]
        public int Id_Empleado { get; set; }
        
        [StringLength(20)]
        public string Clave { get; set; } = string.Empty;
        [StringLength(100)]
        public string Calle { get; set; } = string.Empty;
        [StringLength(20)]
        public string Numero_Ext { get; set; } = string.Empty;
        [StringLength(20)]
        public string Numero_Int { get; set; } = string.Empty;
        [StringLength(100)]
        public string Colonia { get; set; } = string.Empty;
        [StringLength(100)]
        public string Municipio { get; set; } = string.Empty;
        [StringLength(100)]
        public string Estado { get; set; } = string.Empty;
        [StringLength(100)]
        public string Pais { get; set; } = string.Empty;
        [StringLength(100)]
        public string Codigo_Postal { get; set; } = string.Empty;
        [StringLength(20)]
        public string Telefono_Fijo { get; set; } = string.Empty;
        [StringLength(200)]
        public string Referencia { get; set; } = string.Empty;

        public DateTime Fecha_Modificacion { get; set; }
        public Empleado? Empleado { get; set; }
        

    }
}