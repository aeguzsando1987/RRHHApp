using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.ContactosEmpresa
{
    public class ContactosEmpresaUpdateDto
    {
        [Required]
        public int Id_Empresa { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Nombre_Alias { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Domicilio { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Telefono { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Puesto_Ref { get; set; }
    }
}