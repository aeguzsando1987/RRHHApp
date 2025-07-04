using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using RRHH.WebApi.Models.Interfaces;

namespace RRHH.WebApi.Models
{
    public class Empresas_Direccion : IAuditable
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Empresa))]
        public int Id_Empresa { get; set; }
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
        [StringLength(10)]
        public string Codigo_Postal { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Tipo_Direccion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }
        public Empresa? Empresa { get; set; }
    }
}