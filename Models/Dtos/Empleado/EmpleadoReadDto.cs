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

        /// <summary>
        /// Clave del empleado.
        /// </summary>
        public string Clave { get; set; } = string.Empty;

        /// <summary>
        /// Nombres del empleado.
        /// </summary>
        public string Nombres { get; set; } = string.Empty;

        /// <summary>
        /// Apellido paterno del empleado.
        /// </summary>
        public string Apellido_Paterno { get; set; } = string.Empty;

        /// <summary>
        /// Apellido materno del empleado.
        /// </summary>
        public string Apellido_Materno { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del empleado.
        /// </summary>
        public DateTime Fecha_Nacimiento { get; set; }

        /// <summary>
        /// Fecha de inicio del empleado en la empresa.
        /// </summary>
        public DateTime Fecha_Inicio { get; set; }

        /// <summary>
        /// Fecha de termino del empleado en la empresa.
        /// </summary>
        public DateTime? Fecha_Termino { get; set; }

        /// <summary>
        /// Correo corporativo del empleado.
        /// </summary>
        public string Email_corporativo { get; set; } = string.Empty;

        /// <summary>
        /// Telefono del empleado.
        /// </summary>
        public string Tel { get; set; } = string.Empty;

        /// <summary>
        /// Numero de seguridad social del empleado.
        /// </summary>
        public string NSS { get; set; } = string.Empty;

        /// <summary>
        /// RFC del empleado.
        /// </summary>
        public string RFC { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del status del empleado.
        /// </summary>
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

        /// <summary>
        /// Fotografia del empleado.
        /// </summary>
        public byte[]? Fotografia { get; set; }
    }
}