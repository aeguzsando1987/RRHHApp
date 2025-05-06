using System;
using RRHH.WebApi.Models.Dtos.EmpleadoPerfil;

namespace RRHH.WebApi.Models.Dtos.Empleado
{
    /// <summary>
    /// DTO para leer un empleado. Este DTO contiene la informacion necesaria para mostrar
    /// la informacion principal de un empleado.
    /// </summary>
    public class EmpleadoReadDto
    {
        /// <summary>
        /// Identificador unico del empleado.
        /// </summary>
        public int ID { get; set; }

       
        public int Id_Status { get; set; }

        /// <summary>
        /// Identificador del puesto del empleado.
        /// </summary>
        public int Id_Puesto { get; set; }

        /// <summary>
        /// Identificador del jefe del empleado.
        /// </summary>
        public int? Id_Jefe { get; set; }

        /// <summary>
        /// Identificador de la ubicacion del empleado.
        /// </summary>
        public int? Id_Ubicacion { get; set; }

        public EmpleadoPerfilReadDto? Perfil { get; set; }
    }
}