using System;
using System.ComponentModel.DataAnnotations;

namespace RRHH.WebApi.Models.Dtos.PuestosDescriptivo
{


    public class PuestosDescriptivoReadDto
    {
        public int ID { get; set; }
        public int ID_Puesto { get; set; }
        public string Resumen { get; set; } = string.Empty;
        public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;
    }


}