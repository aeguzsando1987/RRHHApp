using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models 
{
    public class ContactosEmpresa
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Empresa))]
        public required int Id_Empresa { get; set; }
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

        public Empresa? Empresa { get; set; }
    }
}