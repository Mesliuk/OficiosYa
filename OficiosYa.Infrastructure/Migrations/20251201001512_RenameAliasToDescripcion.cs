using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAliasToDescripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Alias",
                table: "DireccionesClientes",
                newName: "Descripcion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "DireccionesClientes",
                newName: "Alias");
        }
    }
}
