using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Users {
    public class UserUpdateDto {
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }

        public bool Active { get; set; }
    }
}