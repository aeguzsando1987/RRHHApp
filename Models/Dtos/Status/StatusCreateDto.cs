using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Status
{
    public class StatusCreateDto
    {
        [Required]
        [StringLength(20)]
        public string Status_Emp { get; set;} = string.Empty;

        [StringLength(100)]
        public string Descripcion_Status { get; set; } = string.Empty;
    }

}