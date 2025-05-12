using RRHH.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RRHH.WebApi.Data
{
    public class SeedDataContainer
    {
        public List<Organizacion> Organizaciones { get; set; }
        public List<Empresa> Empresas { get; set; }
        public List<Area> Areas { get; set; }
        public List<Departamento> Departamentos { get; set; }
        public List<Ubicacion> Ubicaciones { get; set; }
        public List<Puesto> Puestos { get; set; }
        public List<Empleado> Empleados { get; set; }
        public List<Empleado_Perfil> EmpleadoPerfiles { get; set; }
        public List<IdentityRole<int>> Roles { get; set; } // Assuming User.cs uses int as PK for IdentityRole
        public List<User> Users { get; set; }
        public List<IdentityUserRole<int>> UserRoles { get; set; }
        public List<Jerarquia> Jerarquias { get; set; }
        public List<Empleado_Tipo> EmpleadoTipos { get; set; } // This was the added line
    }
}