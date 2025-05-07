using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.ContactosEmpleado
{
    public class ContactosEmpleadoReadDto
    {
        public int ID { get; set; }

        public int Id_Empleado { get; set; }

        public string Nombre_Contacto { get; set; } = string.Empty;

        public string Domicilio { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Relacion { get; set; }
    }
}
