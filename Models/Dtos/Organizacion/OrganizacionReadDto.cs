using System;

namespace RRHH.WebApi.Models.Dtos.Organizacion
{


    public class OrganizacionReadDto
    {
        public int Id { get; set; }
        public string? Clave { get; set; }
        public string? Nombre { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }


}