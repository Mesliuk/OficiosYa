using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.Id);
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
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    FotoPerfil = table.Column<string>(type: "text", nullable: true),
                    Rol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oficios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RubroId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    RequiereLicencia = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oficios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oficios_Rubros_RubroId",
                        column: x => x.RubroId,
                        principalTable: "Rubros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmisorId = table.Column<int>(type: "integer", nullable: false),
                    ReceptorId = table.Column<int>(type: "integer", nullable: false),
                    Puntaje = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuarios_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Usuarios_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FotoPerfil = table.Column<string>(type: "text", nullable: true)
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
                    Verificado = table.Column<bool>(type: "boolean", nullable: false),
                    RatingPromedio = table.Column<double>(type: "double precision", nullable: false),
                    TotalCalificaciones = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    FotoPerfil = table.Column<string>(type: "text", nullable: true)
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
                name: "UsuarioRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Rol = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioRole_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
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
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
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
                    NombreDireccion = table.Column<string>(type: "text", nullable: false),
                    Latitud = table.Column<double>(type: "double precision", nullable: false),
                    Longitud = table.Column<double>(type: "double precision", nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.InsertData(
                table: "Rubros",
                columns: new[] { "Id", "Descripcion", "Nombre", "Slug" },
                values: new object[,]
                {
                    { 1, "Trabajos de construcción y mantenimiento", "Construcción y mantenimiento", "construccion-y-mantenimiento" },
                    { 2, "Servicios para vehículos y mecánica", "Vehículos y mecánica", "vehiculos-y-mecanica" },
                    { 3, "Servicios domésticos y para el hogar", "Servicios para el hogar", "servicios-para-el-hogar" },
                    { 4, "Servicios técnicos y digitales", "Tecnología y digital", "tecnologia-y-digital" },
                    { 5, "Servicios de reparación diversos", "Reparaciones varias", "reparaciones-varias" },
                    { 6, "Profesionales de cocina y gastronomía", "Gastronomía", "gastronomia" },
                    { 7, "Oficios manuales y artísticos", "Artes y oficios manuales", "artes-y-oficios-manuales" }
                });

            migrationBuilder.InsertData(
                table: "Oficios",
                columns: new[] { "Id", "Activo", "Descripcion", "Nombre", "RubroId" },
                values: new object[,]
                {
                    { 1, true, "", "Albañil", 1 },
                    { 2, true, "", "Plomero / Gasista (matriculado)", 1 },
                    { 3, true, "", "Electricista", 1 },
                    { 4, true, "", "Pintor", 1 },
                    { 5, true, "", "Carpintero", 1 },
                    { 6, true, "", "Herrero", 1 },
                    { 7, true, "", "Vidriero", 1 },
                    { 8, true, "", "Techista", 1 },
                    { 9, true, "", "Yesero", 1 },
                    { 10, true, "", "Colocador de durlock", 1 },
                    { 11, true, "", "Colocador de cerámica / porcelanato", 1 },
                    { 12, true, "", "Instalador de aire acondicionado", 1 },
                    { 13, true, "", "Instalador de alarmas / cámaras de seguridad", 1 },
                    { 14, true, "", "Jardinero", 1 },
                    { 15, true, "", "Parquero", 1 },
                    { 16, true, "", "Podador de árboles", 1 },
                    { 17, true, "", "Mecánico automotor", 2 },
                    { 18, true, "", "Chapista", 2 },
                    { 19, true, "", "Pintor automotor", 2 },
                    { 20, true, "", "Gomería", 2 },
                    { 21, true, "", "Mecánico de motos", 2 },
                    { 22, true, "", "Electricista automotor", 2 },
                    { 23, true, "", "Lavadero de autos / Detailing", 2 },
                    { 24, true, "", "Limpieza general", 3 },
                    { 25, true, "", "Limpieza profunda", 3 },
                    { 26, true, "", "Limpieza post-obra", 3 },
                    { 27, true, "", "Niñera", 3 },
                    { 28, true, "", "Cuidador de adultos mayores", 3 },
                    { 29, true, "", "Mudanzas / Fletes", 3 },
                    { 30, true, "", "Paseador de perros", 3 },
                    { 31, true, "", "Técnico en PC / Notebook", 4 },
                    { 32, true, "", "Técnico en celulares", 4 },
                    { 33, true, "", "Instalador de redes", 4 },
                    { 34, true, "", "Técnico en impresoras", 4 },
                    { 35, true, "", "Cerrajero", 5 },
                    { 36, true, "", "Tapicero", 5 },
                    { 37, true, "", "Reparación de electrodomésticos", 5 },
                    { 38, true, "", "Servicio técnico de heladeras", 5 },
                    { 39, true, "", "Servicio técnico de lavarropas", 5 },
                    { 40, true, "", "Técnico en TV", 5 },
                    { 41, true, "", "Panadero", 6 },
                    { 42, true, "", "Pastelero", 6 },
                    { 43, true, "", "Cocinero", 6 },
                    { 44, true, "", "Parrillero", 6 },
                    { 45, true, "", "Bartender", 6 },
                    { 46, true, "", "Costurera / Modista", 7 },
                    { 47, true, "", "Sastre", 7 },
                    { 48, true, "", "Artesano", 7 },
                    { 49, true, "", "Zapatero", 7 },
                    { 50, true, "", "Joyería / Relojería", 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_EmisorId",
                table: "Calificaciones",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ReceptorId",
                table: "Calificaciones",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesClientes_ClienteId",
                table: "DireccionesClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Oficios_RubroId",
                table: "Oficios",
                column: "RubroId");

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
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRole_UsuarioId",
                table: "UsuarioRole",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "DireccionesClientes");

            migrationBuilder.DropTable(
                name: "PasswordResetTokens");

            migrationBuilder.DropTable(
                name: "ProfesionalesOficios");

            migrationBuilder.DropTable(
                name: "UbicacionesProfesionales");

            migrationBuilder.DropTable(
                name: "UsuarioRole");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Oficios");

            migrationBuilder.DropTable(
                name: "Profesionales");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
