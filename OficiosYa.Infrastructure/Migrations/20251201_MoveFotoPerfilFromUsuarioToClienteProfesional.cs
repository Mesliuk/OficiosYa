using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    public partial class MoveFotoPerfilFromUsuarioToClienteProfesional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add FotoPerfil to Clientes and Profesionales
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Clientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Profesionales",
                type: "text",
                nullable: true);

            // Remove FotoPerfil from Usuarios (if exists)
            // Use SQL guarded by information_schema to avoid errors if column already removed
            migrationBuilder.Sql(@"DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='Usuarios' AND column_name='FotoPerfil') THEN
        ALTER TABLE ""Usuarios"" DROP COLUMN ""FotoPerfil"";
    END IF;
END
$$;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Recreate FotoPerfil on Usuarios
            migrationBuilder.AddColumn<string>(
                name: "FotoPerfil",
                table: "Usuarios",
                type: "text",
                nullable: true);

            // Remove from Clientes and Profesionales
            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "FotoPerfil",
                table: "Profesionales");
        }
    }
}
