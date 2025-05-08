using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.Users
{
    public class UserCreateDto
    {
        [Required]
        public int Id_Empleado { get; set; }
        [Required]
        public required string Username { get; set; }

        [Required]

        public required string Password { get; set; }

        public bool Active { get; set; }
    }
}
