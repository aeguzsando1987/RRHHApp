using System.ComponentModel.DataAnnotations;
using RRHH.WebApi.Models.Dtos.EmpleadoPerfil;

namespace RRHH.WebApi.Models.Dtos.Empleado
{
    public class EmpleadoUpdateDto
    {
        public int Id_Puesto { get; set; }
        
        public int? Id_Jefe { get; set; }
        
        public int? Id_Ubicacion { get; set; }
        
        public EmpleadoPerfilUpdateDto? Perfil { get; set; }
        
        
    }
}