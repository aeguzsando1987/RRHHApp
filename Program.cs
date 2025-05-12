using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
// Directiva using para el espacio de nombres de datos
using RRHH.WebApi.Data;
using RRHH.WebApi.Repositories;
using RRHH.WebApi.Repositories.Interfaces;
// Directiva using para el espacio de nombres de Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity; 
using RRHH.WebApi.Models; 
using RRHH.WebApi.Services;


// Crear un constructor de aplicaciones web para configurar servicios y middleware
var builder = WebApplication.CreateBuilder(args);
// Obtener la configuración JWT desde appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// Convertir la clave secreta JWT a bytes para usarla en la generación de tokens
var secretKey = Encoding.ASCII.GetBytes(jwtSettings["Key"]!); // Aseguramos que Key no sea null

// Configurar el servicio de autenticación con JWT Bearer
builder.Services.AddAuthentication(options =>
{
    // Establecer JWT Bearer como esquema por defecto para autenticar solicitudes
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    // Establecer JWT Bearer como esquema por defecto para desafíos de autenticación
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // Establecer JWT Bearer como esquema predeterminado general
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // No requerir HTTPS en metadatos para entorno de desarrollo
    options.RequireHttpsMetadata = false;
    // Guardar el token para su uso posterior en la solicitud
    options.SaveToken = true;
    // Configurar los parámetros de validación del token JWT
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Verificar que el token está firmado con la clave correcta
        ValidateIssuerSigningKey = true,
        // Establecer la clave utilizada para verificar la firma del token
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        // Verificar que el emisor del token es válido
        ValidateIssuer = true,
        // Establecer el emisor válido desde la configuración
        ValidIssuer = jwtSettings["Issuer"],
        // Verificar que la audiencia del token es válida
        ValidateAudience = true,
        // Establecer la audiencia válida desde la configuración
        ValidAudience = jwtSettings["Audience"],
        // Verificar que el token no ha expirado
        ValidateLifetime = true,
        // Eliminar el tiempo de gracia predeterminado para la expiración del token
        ClockSkew = TimeSpan.Zero
    };
});

// Agregar servicios al contenedor
// Obtener más información sobre la configuración de Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
// Agregar el contexto de la base de datos
builder.Services.AddDbContext<RRHHDbContext>(options =>
{
    // Usar la base de datos de SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure ASP.NET Core Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // You can configure Identity options here, for example:
    // options.Password.RequireDigit = true;
    // options.Password.RequiredLength = 8;
    // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    // options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<RRHHDbContext>()
.AddDefaultTokenProviders();

// Agregar los repositorios y servicios
builder.Services.AddScoped<OrganizacionRepository>();
builder.Services.AddScoped<EmpresasRepository>();
builder.Services.AddScoped<AreaRepository>();
builder.Services.AddScoped<DepartamentoRepository>();
builder.Services.AddScoped<JerarquiaRepository>();
builder.Services.AddScoped<PuestoRepository>();
builder.Services.AddScoped<PuestosDescriptivoRepository>();
builder.Services.AddScoped<PuestosActividadRepository>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<EmpleadoTipoRepository>();
builder.Services.AddScoped<UbicacionRepository>();
builder.Services.AddScoped<ContactosEmpresaRepository>();
builder.Services.AddScoped<Empleados_DireccionRepository>();
builder.Services.AddScoped<Empresas_DireccionRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();

// Agregar los controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.MaxDepth = 32;
    });

// Agregar los generadores de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    // Usar Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enrutamiento
app.UseRouting();

// Usar autenticación
app.UseAuthentication();

// Usar autorización
app.UseAuthorization();

// Mapear los controladores
app.MapControllers();

// Usar HTTPS
app.UseHttpsRedirection();



// Crear un array de cadenas
var resumenes = new[]
{
    "Helado", "Refrescante", "Frío", "Fresco", "Suave", "Cálido", "Agradable", "Caliente", "Suficiente", "Ardiente"
};

// Mapear el pronóstico del clima
app.MapGet("/weatherforecast", () =>
{
    // Crear un array de objetos WeatherForecast
    var pronostico =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            // Obtener la fecha
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            // Obtener la temperatura en grados Celsius
            Random.Shared.Next(-20, 55),
            // Obtener un resumen aleatorio
            resumenes[Random.Shared.Next(resumenes.Length)]
        ))
        .ToArray();
    // Devolver el pronóstico
    return pronostico;
})
.WithName("GetWeatherForecast")
.WithOpenApi();


// Iniciar la aplicación
app.Run();

// Tipo de registro para el pronóstico del clima
record WeatherForecast(DateOnly Fecha, int TemperaturaC, string? Resumen)
{
    // Propiedad para obtener la temperatura en grados Fahrenheit
    public int TemperaturaF => 32 + (int)(TemperaturaC / 0.5556);
}
