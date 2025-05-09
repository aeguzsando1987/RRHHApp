namespace RRHH.WebApi.Models.Dtos.Users {
    public class UserReadDto {
        public int ID { get; set; }
        public int Id_Empleado { get; set; }
        public required string Username { get; set; }
        // public required string Password { get; set; } // Removed
        public bool Active { get; set; }
    }
}