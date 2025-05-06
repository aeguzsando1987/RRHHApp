using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.EmpladoTipo;

public class EmpleadoTipoReadDto {
    public int ID { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Prefijo { get; set; } = string.Empty;
}


