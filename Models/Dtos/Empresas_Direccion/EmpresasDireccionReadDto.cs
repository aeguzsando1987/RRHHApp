
namespace RRHH.WebApi.Models.Dtos.Empresas_Direccion
{
    public class EmpresasDireccionReadDto
    {
        public int ID { get; set; }
        public int Id_Empresa { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string Numero_Ext { get; set; } = string.Empty;
        public string? Numero_Int { get; set; }
        public string Colonia { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Codigo_Postal { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Tipo_Direccion { get; set; }
        public DateTime Fecha_Modificacion { get; set; }
    }
}