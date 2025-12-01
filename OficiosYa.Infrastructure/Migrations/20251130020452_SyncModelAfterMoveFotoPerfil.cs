using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelAfterMoveFotoPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FotoPerfil",
                table: "Usuarios",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Profesionales",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Clientes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Profesionales");

            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Clientes");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FotoPerfil",
                table: "Usuarios",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
