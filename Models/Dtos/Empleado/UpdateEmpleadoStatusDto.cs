using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Empleado
{
    public class UpdateEmpleadoStatusDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de estatus debe ser un número positivo.")]
        public int NewStatusId { get; set; }
    }
}
