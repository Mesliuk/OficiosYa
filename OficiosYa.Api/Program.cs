using Microsoft.EntityFrameworkCore;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Services;
using OficiosYa.Infrastructure.Persistence;
using OficiosYa.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// DbContext
builder.Services.AddDbContext<OficiosYaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ====================================
// REPOSITORIES (Infrastructure → Application.Interfaces)
// ====================================
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<IOficioRepository, OficioRepository>();
builder.Services.AddScoped<IClasificacionRepository, CalificacionRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();

// ====================================
// APPLICATION SERVICES
// ====================================
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ProfesionalService>();
builder.Services.AddScoped<OficioService>();
builder.Services.AddScoped<CalificacionService>();
builder.Services.AddScoped<UbicacionService>();
// Registro correcto contra la interfaz
builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();

// ====================================
// HANDLERS
// ====================================
// Usuarios
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.RegisterClienteHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.RegisterProfesionalHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.LoginUsuarioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.ResetPasswordHandler>();

// Cliente
builder.Services.AddScoped<OficiosYa.Application.Handlers.Cliente.UpdateClienteHandler>();

// Profesional
builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.UpdateProfesionalHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.GetProfesionalByIdHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.SearchProfesionalHandler>();

// Oficios
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.GetAllOficiosHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.CreateOficioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.UpdateOficioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.DeleteOficioHandler>();

// Ubicacion
builder.Services.AddScoped<OficiosYa.Application.Handlers.Ubicacion.RegisterUbicacionHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Ubicacion.GetUbicacionHandler>();

WebApplication app = builder.Build();

// Middleware
app.UseOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class PasswordResetService : IPasswordResetService
{
    public Task<string> GenerarTokenAsync(int usuarioId) => Task.FromResult("");
    public Task<bool> ValidarTokenAsync(string token) => Task.FromResult(false);
    public Task<bool> ResetPasswordAsync(string token, string nuevoPassword) => Task.FromResult(false);
}

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}

