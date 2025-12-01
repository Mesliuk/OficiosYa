using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUsuarioYCalificacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DireccionesClientes_Usuarios_UsuarioId",
                table: "DireccionesClientes");

            migrationBuilder.DropIndex(
                name: "IX_DireccionesClientes_UsuarioId",
                table: "DireccionesClientes");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "DireccionesClientes");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Profesionales",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Profesionales");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Profesionales",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "DireccionesClientes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DireccionesClientes_UsuarioId",
                table: "DireccionesClientes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_DireccionesClientes_Usuarios_UsuarioId",
                table: "DireccionesClientes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
