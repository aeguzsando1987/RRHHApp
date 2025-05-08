using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Empresas_Direccion;

public class EmpresasDireccionCreateDto
{
    [Required]
    public int Id_Empresa { get; set; }
    [Required]
    [StringLength(20)]
    public string Clave { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Calle { get; set; } = string.Empty;
    [Required]
    [StringLength(20)]
    public string Numero_Ext { get; set; } = string.Empty;
    [StringLength(20)]
    public string? Numero_Int { get; set; }
    [Required]
    [StringLength(100)]
    public string Colonia { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Municipio { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Estado { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Pais { get; set; } = string.Empty;
    [Required]
    [StringLength(10)]
    public string Codigo_Postal { get; set; } = string.Empty;
    public string? Referencia { get; set; }
    public string? Tipo_Direccion { get; set; }
}