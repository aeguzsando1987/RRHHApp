using System;
using Microsoft.Identity.Client;
using RRHH.WebApi.Models.Dtos.EmpladoTipo;



namespace RRHH.WebApi.Models.Dtos.EmpleadoPerfil
{
    public class EmpleadoPerfilReadDto
    {
        public int Id_Empleado { get; set; }
       
        public string Clave { get; set; } = string.Empty;

        public string Nombres { get; set; } = string.Empty;

        public string Apellido_Paterno { get; set; } = string.Empty;

        public string Apellido_Materno { get; set; } = string.Empty;

        public DateTime? Fecha_Nacimiento { get; set;}

        public string? Sexo { get; set; }

        public string? Edo_Civil { get; set; }

        public DateTime Fecha_Inicio { get; set;}

        public DateTime? Fecha_Termino { get; set;}

        public string? Email { get; set; }

        public string? Tel { get; set; }

        public string? NSS { get; set; }

        public string? RFC { get; set; }

        public string? Curp { get; set; } 

        public byte[]? Fotografia { get; set; }

        public int Id_Tipo_Empleado { get; set; }

        public EmpleadoTipoReadDto? Tipo_Empleado { get; set; }

    }
}

