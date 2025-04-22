using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.WebApi.Models {

    public class User {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Empleado))]
        public int Id_Empleado { get; set; }

        [Required, StringLength(50)]
        public required string Username { get; set; }

        [Required, StringLength(50)]
        public required string Password { get; set; }
        
        public bool Active { get; set; }

        public Empleado Empleado { get; set; } = null!;
    }

}