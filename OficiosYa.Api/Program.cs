using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OficiosYa.Application.Interfaces;
using OficiosYa.Application.Services;
using OficiosYa.Infrastructure.Persistence;
using OficiosYa.Infrastructure.Repositories;
using OficiosYa.Api.Middleware;
using Microsoft.Extensions.FileProviders;
using OficiosYa.Api.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;
using OficiosYa.Api.Services;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Configure logging to console for easier debugging (Developer Exception output)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(OficiosYa.Api.Mappings.RegistrationProfile));

// Add controllers with JSON options
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
})
.AddJsonOptions(opts => opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// FluentValidation: register validators but DO NOT enable auto-validation (we will validate manually in controllers)
// Note: previously AddFluentValidationAutoValidation/AddFluentValidationClientsideAdapters were enabled; removed intentionally
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddOpenApi();

// Configure JWT options
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key") ?? "default_dev_key_change_this";
var jwtIssuer = jwtSection.GetValue<string>("Issuer") ?? "OficiosYa";
var jwtAudience = jwtSection.GetValue<string>("Audience") ?? "OficiosYaAudience";

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// DbContext
builder.Services.AddDbContext<OficiosYaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNext", policy =>  // ← Darle nombre
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ====================================
// REPOSITORIES (Infrastructure → Application.Interfaces)
// ====================================
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProfesionalRepository, ProfesionalRepository>();
builder.Services.AddScoped<IOficioRepository, OficioRepository>();
builder.Services.AddScoped<IClasificacionRepository, CalificacionRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<IRubroRepository, RubroRepository>(); // Rubros

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

// Register photo service
builder.Services.AddScoped<IPhotoService, LocalPhotoService>();

// ====================================
// HANDLERS
// ====================================
// Usuarios
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.RegisterClienteHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.RegisterProfesionalHandler>();
// Login handler now needs IConfiguration, make sure it's resolvable
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.LoginUsuarioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Usuarios.ResetPasswordHandler>();

// Cliente
builder.Services.AddScoped<OficiosYa.Application.Handlers.Cliente.UpdateClienteHandler>();

// Profesional
// builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.UpdateProfesionalHandler>();
// builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.GetProfesionalByIdHandler>();
// builder.Services.AddScoped<OficiosYa.Application.Handlers.Profesional.SearchProfesionalHandler>();
// Profesional handlers removed because ProfesionalController was deleted
// If needed later, re-register handlers here

// Oficios
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.GetAllOficiosHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.CreateOficioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.UpdateOficioHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Oficios.DeleteOficioHandler>();

// Rubros
builder.Services.AddScoped<OficiosYa.Application.Handlers.Rubros.GetRubrosHandler>();

// Ubicacion
builder.Services.AddScoped<OficiosYa.Application.Handlers.Ubicacion.RegisterUbicacionHandler>();
builder.Services.AddScoped<OficiosYa.Application.Handlers.Ubicacion.GetUbicacionHandler>();

// Configure Swagger to include JWT Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[] {}
        }
    });
});

var app = builder.Build();

// Run migrations and seed rubros on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<OficiosYaDbContext>();
        db.Database.Migrate();
        // Seed default rubros
        await OficiosYa.Infrastructure.Seed.RubroSeeder.SeedAsync(db);
    }
    catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger?.LogError(ex, "Error al aplicar migraciones o sembrar datos");
        throw;
    }
}

// Serve static files (wwwroot)
app.UseStaticFiles();
// Ensure uploads folder is served at /uploads
var env = app.Environment;
var uploadsPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
Directory.CreateDirectory(uploadsPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
});

// Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Show developer exception page in Development to surface errors when generating swagger.json
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseOpenApi();
app.UseCors("AllowNext");
app.UseHttpsRedirection();
app.UseAuthentication();
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
        return services;
    }

    public static WebApplication UseOpenApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}



