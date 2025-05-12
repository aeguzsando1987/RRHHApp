using Microsoft.Extensions.Configuration; // Para acceder a la configuración de la aplicación
using Microsoft.IdentityModel.Tokens; // Para usar tipos de seguridad como SymmetricSecurityKey
using RRHH.WebApi.Models; // Para acceder al modelo User
using System; // Para usar tipos básicos como DateTime
using System.Collections.Generic; // Para usar colecciones como List
using System.IdentityModel.Tokens.Jwt; // Para manejar tokens JWT
using System.Linq; // Para usar métodos de extensión como Any()
using System.Security.Claims; // Para crear y manejar Claims
using System.Text; // Para usar Encoding

namespace RRHH.WebApi.Services // Espacio de nombres para servicios de la aplicación
{
    /// <summary>
    /// Servicio encargado de generar tokens JWT para la autenticación de usuarios.
    /// Implementa la interfaz ITokenService para proporcionar funcionalidad de creación de tokens.
    /// </summary>
    public class TokenService : ITokenService // Clase que implementa la interfaz ITokenService
    {
        private readonly SymmetricSecurityKey _key; // Clave simétrica para firmar el token
        private readonly string _issuer; // Emisor del token JWT
        private readonly string _audience; // Audiencia del token JWT
        private readonly double _durationInMinutes; // Duración del token en minutos

        /// <summary>
        /// Constructor que inicializa el servicio con la configuración de la aplicación.
        /// </summary>
        /// <param name="config">Configuración de la aplicación que contiene los ajustes JWT</param>
        public TokenService(IConfiguration config) // Constructor que recibe la configuración como parámetro
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)); // Crea la clave simétrica a partir de la configuración
            _issuer = config["JwtSettings:Issuer"]!; // Obtiene el emisor desde la configuración
            _audience = config["JwtSettings:Audience"]!; // Obtiene la audiencia desde la configuración
            _durationInMinutes = Convert.ToDouble(config["JwtSettings:DurationInMinutes"]); // Convierte la duración a double
        }

        /// <summary>
        /// Crea un token JWT para un usuario con sus roles asociados.
        /// </summary>
        /// <param name="user">Usuario para el cual se generará el token</param>
        /// <param name="roles">Lista de roles asignados al usuario</param>
        /// <returns>Token JWT en formato string</returns>
        public string CreateToken(User user, IList<string> roles) // Método para crear el token JWT
        {
            var claims = new List<Claim> // Crea una lista de claims para incluir en el token
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()), // Agrega el ID del usuario como claim
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!) // Agrega el nombre de usuario como claim
                // Potentially add JwtRegisteredClaimNames.Email if you store and manage email via Identity
                // new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            // Add role claims
            if (roles != null && roles.Any()) // Verifica si hay roles para agregar
            {
                foreach (var role in roles) // Itera sobre cada rol
                {
                    claims.Add(new Claim(ClaimTypes.Role, role)); // Agrega cada rol como un claim
                }
            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // Crea las credenciales de firma con la clave

            var tokenDescriptor = new SecurityTokenDescriptor // Crea el descriptor del token con sus propiedades
            {
                Subject = new ClaimsIdentity(claims), // Establece los claims como la identidad del sujeto
                Expires = DateTime.UtcNow.AddMinutes(_durationInMinutes), // Establece la fecha de expiración
                Issuer = _issuer, // Establece el emisor del token
                Audience = _audience, // Establece la audiencia del token
                SigningCredentials = creds // Establece las credenciales de firma
            };

            var tokenHandler = new JwtSecurityTokenHandler(); // Crea un manejador de tokens JWT

            var token = tokenHandler.CreateToken(tokenDescriptor); // Crea el token usando el descriptor

            return tokenHandler.WriteToken(token); // Convierte el token a string y lo devuelve
        }
    }
}
