using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_SolicitudesServicio_SolicitudId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Usuarios_UsuarioCalificaId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Usuarios_UsuarioCalificadoId",
                table: "Calificaciones");

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
                name: "Trabajadores");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_SolicitudId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IconoUrl",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "SolicitudId",
                table: "Calificaciones");

            migrationBuilder.RenameColumn(
                name: "UsuarioCalificadoId",
                table: "Calificaciones",
                newName: "ReceptorId");

            migrationBuilder.RenameColumn(
                name: "UsuarioCalificaId",
                table: "Calificaciones",
                newName: "EmisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Calificaciones_UsuarioCalificaId",
                table: "Calificaciones",
                newName: "IX_Calificaciones_EmisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Calificaciones_UsuarioCalificadoId",
                table: "Calificaciones",
                newName: "IX_Calificaciones_ReceptorId");

            migrationBuilder.AddColumn<int>(
                name: "Rol",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Oficios",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Calificaciones",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfesionalId",
                table: "Calificaciones",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expira = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Usado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasswordResetTokens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    Verificado = table.Column<bool>(type: "boolean", nullable: false),
                    RatingPromedio = table.Column<double>(type: "double precision", nullable: false),
                    TotalCalificaciones = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profesionales_Usuarios_Id",
                        column: x => x.Id,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DireccionesClientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    Alias = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DireccionesClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DireccionesClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfesionalesOficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: false),
                    OficioId = table.Column<int>(type: "integer", nullable: false),
                    AnosExperiencia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesionalesOficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfesionalesOficios_Oficios_OficioId",
                        column: x => x.OficioId,
                        principalTable: "Oficios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesionalesOficios_Profesionales_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UbicacionesProfesionales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UbicacionesProfesionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UbicacionesProfesionales_Profesionales_ProfesionalId",
                        column: x => x.ProfesionalId,
                        principalTable: "Profesionales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ClienteId",
                table: "Calificaciones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ProfesionalId",
                table: "Calificaciones",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesClientes_ClienteId",
                table: "DireccionesClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UsuarioId",
                table: "PasswordResetTokens",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalesOficios_OficioId",
                table: "ProfesionalesOficios",
                column: "OficioId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesionalesOficios_ProfesionalId",
                table: "ProfesionalesOficios",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_UbicacionesProfesionales_ProfesionalId",
                table: "UbicacionesProfesionales",
                column: "ProfesionalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Clientes_ClienteId",
                table: "Calificaciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Profesionales_ProfesionalId",
                table: "Calificaciones",
                column: "ProfesionalId",
                principalTable: "Profesionales",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Usuarios_EmisorId",
                table: "Calificaciones",
                column: "EmisorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Usuarios_ReceptorId",
                table: "Calificaciones",
                column: "ReceptorId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Clientes_ClienteId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Profesionales_ProfesionalId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Usuarios_EmisorId",
                table: "Calificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Calificaciones_Usuarios_ReceptorId",
                table: "Calificaciones");

            migrationBuilder.DropTable(
                name: "DireccionesClientes");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "ProfesionalesOficios");

            migrationBuilder.DropTable(
                name: "UbicacionesProfesionales");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_ClienteId",
                table: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_Calificaciones_ProfesionalId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Calificaciones");

            migrationBuilder.DropColumn(
                name: "ProfesionalId",
                table: "Calificaciones");

            migrationBuilder.RenameColumn(
                name: "ReceptorId",
                table: "Calificaciones",
                newName: "UsuarioCalificadoId");

            migrationBuilder.RenameColumn(
                name: "EmisorId",
                table: "Calificaciones",
                newName: "UsuarioCalificaId");

            migrationBuilder.RenameIndex(
                name: "IX_Calificaciones_ReceptorId",
                table: "Calificaciones",
                newName: "IX_Calificaciones_UsuarioCalificadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Calificaciones_EmisorId",
                table: "Calificaciones",
                newName: "IX_Calificaciones_UsuarioCalificaId");

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Usuarios",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Oficios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "IconoUrl",
                table: "Oficios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SolicitudId",
                table: "Calificaciones",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
                    Bio = table.Column<string>(type: "text", nullable: true),
                    Documento = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Genero = table.Column<string>(type: "text", nullable: true),
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
                    DireccionId = table.Column<int>(type: "integer", nullable: false),
                    OficioId = table.Column<int>(type: "integer", nullable: false),
                    TrabajadorAsignadoId = table.Column<int>(type: "integer", nullable: true),
                    DescripcionProblema = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetodoPago = table.Column<int>(type: "integer", nullable: false),
                    PrecioEstimado = table.Column<decimal>(type: "numeric", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "numeric", nullable: true)
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
                    OficioId = table.Column<int>(type: "integer", nullable: false),
                    TrabajadorId = table.Column<int>(type: "integer", nullable: false),
                    AnosExperiencia = table.Column<int>(type: "integer", nullable: false),
                    CertificadosUrl = table.Column<string>(type: "text", nullable: true),
                    PrecioHoraBase = table.Column<decimal>(type: "numeric", nullable: false)
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
                name: "SolicitudesCandidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SolicitudId = table.Column<int>(type: "integer", nullable: false),
                    TrabajadorId = table.Column<int>(type: "integer", nullable: false),
                    DistanciaCliente = table.Column<double>(type: "double precision", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_SolicitudesServicio_SolicitudId",
                table: "Calificaciones",
                column: "SolicitudId",
                principalTable: "SolicitudesServicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Usuarios_UsuarioCalificaId",
                table: "Calificaciones",
                column: "UsuarioCalificaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calificaciones_Usuarios_UsuarioCalificadoId",
                table: "Calificaciones",
                column: "UsuarioCalificadoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
