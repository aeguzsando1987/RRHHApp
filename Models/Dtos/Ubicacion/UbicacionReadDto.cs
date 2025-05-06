namespace RRHH.WebApi.Models.Dtos.Ubicacion
{
    public class UbicacionReadDto
    {
        public int ID { get; set; }
        public int Id_Empresa { get; set; }
        public string?   Clave { get; set; }
        public string? Ubicacion_Referencial { get; set; }
    }
}