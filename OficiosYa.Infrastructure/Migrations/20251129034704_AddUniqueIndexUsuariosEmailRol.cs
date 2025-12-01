using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexUsuariosEmailRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Oficios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Oficios",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiereLicencia",
                table: "Oficios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RubroId",
                table: "Oficios",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Oficios_RubroId",
                table: "Oficios",
                column: "RubroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Oficios_Rubros_RubroId",
                table: "Oficios",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oficios_Rubros_RubroId",
                table: "Oficios");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropIndex(
                name: "IX_Oficios_RubroId",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "RequiereLicencia",
                table: "Oficios");

            migrationBuilder.DropColumn(
                name: "RubroId",
                table: "Oficios");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Oficios",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
