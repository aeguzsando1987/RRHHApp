// Directiva using para el espacio de nombres de datos
using RRHH.WebApi.Data;
using RRHH.WebApi.Repositories;
// Directiva using para el espacio de nombres de Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

// Crear un nuevo constructor de aplicaciones web
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor
// Obtener más información sobre la configuración de Swagger/OpenAPI en https://aka.ms/aspnetcore/swashbuckle
// Agregar el contexto de la base de datos
builder.Services.AddDbContext<RRHHDbContext>(options =>
{
    // Usar la base de datos de SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Agregar los repositorios
builder.Services.AddScoped<OrganizacionRepository>();
builder.Services.AddScoped<EmpresasRepository>();
builder.Services.AddScoped<AreaRepository>();
builder.Services.AddScoped<DepartamentoRepository>();
builder.Services.AddScoped<JerarquiaRepository>();
builder.Services.AddScoped<PuestoRepository>();
builder.Services.AddScoped<PuestosDescriptivoRepository>();
builder.Services.AddScoped<PuestosActividadRepository>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<EmpleadoRepository>();
builder.Services.AddScoped<EmpleadoTipoRepository>();
builder.Services.AddScoped<UbicacionRepository>();
builder.Services.AddScoped<ContactosEmpresaRepository>();
builder.Services.AddScoped<Empleados_DireccionRepository>();
builder.Services.AddScoped<Empresas_DireccionRepository>();
builder.Services.AddScoped<UserRepository>();

// Agregar los controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.MaxDepth = 32;
    })
    .AddNewtonsoftJson();

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
