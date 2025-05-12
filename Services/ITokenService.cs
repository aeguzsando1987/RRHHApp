using RRHH.WebApi.Models;
using System.Collections.Generic;

namespace RRHH.WebApi.Services
{
    /// <summary>
    /// Interfaz que define el servicio de generación de tokens JWT.
    /// </summary>
    /// <remarks>
    /// Esta interfaz es responsable de la creación de tokens de autenticación JWT (JSON Web Tokens)
    /// que son utilizados para la autenticación y autorización de usuarios en la aplicación.
    /// Se implementa como una interfaz para permitir la inyección de dependencias y facilitar las pruebas unitarias.
    /// 
    /// Esta interfaz es implementada por la clase TokenService en TokenService.cs, la cual proporciona
    /// la lógica concreta para crear tokens JWT con claims específicos, configurando la firma,
    /// el tiempo de expiración, emisor y audiencia según la configuración de la aplicación.
    /// </remarks>
    public interface ITokenService
    {
        /// <summary>
        /// Genera un token JWT para un usuario con sus roles asociados.
        /// </summary>
        /// <param name="user">Usuario para el cual se generará el token</param>
        /// <param name="roles">Lista de roles asignados al usuario</param>
        /// <returns>Token JWT en formato string</returns>
        string CreateToken(User user, IList<string> roles);
    }
}
