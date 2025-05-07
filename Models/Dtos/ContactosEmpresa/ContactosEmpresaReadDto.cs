namespace RRHH.WebApi.Models.Dtos.ContactosEmpresa
{
    public class ContactosEmpresaReadDto
    {
        public int ID { get; set; }
        public int Id_Empresa { get; set; }
        public string Nombre_Alias { get; set; } = string.Empty;
        public string Domicilio { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Puesto_Ref { get; set; }

    }
}