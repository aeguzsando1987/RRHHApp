using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Ubicacion
{
    public class UbicacionCreateDto
    {
        [StringLength(50)]
        public string? Clave { get; set; }

        [Required]
        [StringLength(200)]
        public string? Ubicacion_Referencial { get; set; }

        [Required]
        public int Id_Empresa { get; set; }
    }
}
