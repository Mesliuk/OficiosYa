using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.CreateTable(
                name: "Oficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    IconoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DireccionesUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionesUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Genero = table.Column<string>(type: "text", nullable: true),
                    Bio = table.Column<string>(type: "text", nullable: true),
                    RatingPromedio = table.Column<double>(type: "double precision", nullable: false),
                    TotalTrabajosRealizados = table.Column<int>(type: "integer", nullable: false),
                    Verificado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajadores_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Rol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    OficioId = table.Column<int>(type: "integer", nullable: false),
                    DescripcionProblema = table.Column<string>(type: "text", nullable: false),
                    DireccionId = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    PrecioEstimado = table.Column<decimal>(type: "numeric", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "numeric", nullable: true),
                    TrabajadorAsignadoId = table.Column<int>(type: "integer", nullable: true),
                    MetodoPago = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_DireccionesUsuario_DireccionId",
                        column: x => x.DireccionId,
                        principalTable: "DireccionesUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_Oficios_OficioId",
                        column: x => x.OficioId,
                        principalTable: "Oficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_Trabajadores_TrabajadorAsignadoId",
                        column: x => x.TrabajadorAsignadoId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajadoresOficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrabajadorId = table.Column<int>(type: "integer", nullable: false),
                    OficioId = table.Column<int>(type: "integer", nullable: false),
                    AnosExperiencia = table.Column<int>(type: "integer", nullable: false),
                    PrecioHoraBase = table.Column<decimal>(type: "numeric", nullable: false),
                    CertificadosUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadoresOficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadoresOficios_Oficios_OficioId",
                        column: x => x.OficioId,
                        principalTable: "Oficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadoresOficios_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionesTrabajadores",
                columns: table => new
                {
                    TrabajadorId = table.Column<int>(type: "integer", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesTrabajadores", x => x.TrabajadorId);
                    table.ForeignKey(
                        name: "FK_UbicacionesTrabajadores_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolicitudId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioCalificaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioCalificadoId = table.Column<int>(type: "integer", nullable: false),
                    Puntaje = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_SolicitudesServicio_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuarios_UsuarioCalificaId",
                        column: x => x.UsuarioCalificaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuarios_UsuarioCalificadoId",
                        column: x => x.UsuarioCalificadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesCandidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolicitudId = table.Column<int>(type: "integer", nullable: false),
                    TrabajadorId = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    DistanciaCliente = table.Column<double>(type: "double precision", nullable: false),
                    TiempoEstimadoLlegada = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesCandidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudesCandidatos_SolicitudesServicio_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "SolicitudesServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesCandidatos_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_SolicitudId",
                table: "Calificaciones",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioCalificadoId",
                table: "Calificaciones",
                column: "UsuarioCalificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_UsuarioCalificaId",
                table: "Calificaciones",
                column: "UsuarioCalificaId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesUsuario_UsuarioId",
                table: "DireccionesUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCandidatos_SolicitudId",
                table: "SolicitudesCandidatos",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesCandidatos_TrabajadorId",
                table: "SolicitudesCandidatos",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_ClienteId",
                table: "SolicitudesServicio",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_DireccionId",
                table: "SolicitudesServicio",
                column: "DireccionId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_OficioId",
                table: "SolicitudesServicio",
                column: "OficioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_TrabajadorAsignadoId",
                table: "SolicitudesServicio",
                column: "TrabajadorAsignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresOficios_OficioId",
                table: "TrabajadoresOficios",
                column: "OficioId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadoresOficios_TrabajadorId",
                table: "TrabajadoresOficios",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_UsuarioId",
                table: "UsuariosRoles",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "SolicitudesCandidatos");

            migrationBuilder.DropTable(
                name: "TrabajadoresOficios");

            migrationBuilder.DropTable(
                name: "UbicacionesTrabajadores");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "SolicitudesServicio");

            migrationBuilder.DropTable(
                name: "DireccionesUsuario");

            migrationBuilder.DropTable(
                name: "Oficios");

            migrationBuilder.DropTable(
                name: "Trabajadores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.CreateTable(
                name: "Profesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PrecioHora = table.Column<decimal>(type: "numeric", nullable: false),
                    Rubro = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesionales", x => x.Id);
                });
        }
    }
}
