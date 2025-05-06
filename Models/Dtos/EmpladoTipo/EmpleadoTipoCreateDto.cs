using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.EmpladoTipo{
    public class EmpleadoTipoCreateDto {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string? Descripcion { get; set;}
        [Required]
        [StringLength(20)]
        public string Prefijo { get; set; } = string.Empty;
    }
}