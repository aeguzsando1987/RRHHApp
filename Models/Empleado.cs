namespace RRHH.WebApi.Models {

    public class Empleado {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string Ciudad { get; set; }
        public string Pais { get; set; }
        public string Foto { get; set; }
    }

}