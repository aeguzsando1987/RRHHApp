using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using RRHH.WebApi.Models.Interfaces;

namespace RRHH.WebApi.Models
{
        /// <summary>
        /// Entidad que representa a una direccion de un empleado.
        /// </summary>
        /// <remarks>
        /// La clave del empleado se genera automaticamente con el formato
        /// <c>Prefix{Id_Empleado}</c>, donde <c>Prefix</c> depende del tipo de empleado.
        /// </remarks>
        
    public class Empleados_Direccion : IAuditable
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
        public string? Numero_Int { get; set; }
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
        public string? Telefono_Fijo { get; set; }
        [StringLength(200)]
        public string? Referencia { get; set; }

        public DateTime Fecha_Modificacion { get; set; }
        public Empleado? Empleado { get; set; }
        

    }
}