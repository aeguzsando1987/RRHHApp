namespace RRHH.WebApi.Models.Dtos.Empleados_Direccion
{
    public class EmpleadosDireccionReadDto
    {
        public int ID { get; set; }
        public int Id_Empleado { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string Numero_Ext { get; set; } = string.Empty;
        public string? Numero_Int { get; set; }
        public string Colonia { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Codigo_Postal { get; set; } = string.Empty;
        public string? Telefono_Fijo { get; set; }
        public string? Referencia { get; set; }
        public DateTime Fecha_Modificacion { get; set; }
    }
}