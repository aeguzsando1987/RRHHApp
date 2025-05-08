using System;

namespace RRHH.WebApi.Models.Interfaces
{
    /// <summary>
    /// Interface que define una entidad que puede ser modificada.
    /// </summary>
    public interface IAuditable
    {
        DateTime Fecha_Modificacion { get; set; }
    }
}
