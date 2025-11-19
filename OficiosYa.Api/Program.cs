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
builder.Services.AddScoped<ICalificacionRepository, CalificacionRepository>();
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
builder.Services.AddScoped<PasswordResetService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

