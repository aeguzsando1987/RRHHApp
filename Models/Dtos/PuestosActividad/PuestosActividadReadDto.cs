using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace RRHH.WebApi.Models.Dtos.PuestosActividad
{

    /// <summary>
    /// DTO para leer las actividades de un puesto descriptivo.
    /// </summary>
    public class PuestosActividadReadDto
    {
        /// <summary>
        /// Identificador unico de la actividad.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Identificador del puesto descriptivo al que pertenece.
        /// </summary>
        public int ID_PuestosDescriptivo { get; set; }
        /// <summary>
        /// Descripcion de la actividad.
        /// </summary>
        public string Titulo { get; set; } = string.Empty;
        public string Resumen { get; set; } = string.Empty;

        public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;
    }

}