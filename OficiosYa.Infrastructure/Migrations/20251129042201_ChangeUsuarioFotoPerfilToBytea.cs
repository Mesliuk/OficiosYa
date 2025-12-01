using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficiosYa.Infrastructure.Migrations
{
    public partial class ChangeUsuarioFotoPerfilToBytea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing FotoPerfil and recreate as bytea (safe when no data to preserve)
            migrationBuilder.Sql("ALTER TABLE \"Usuarios\" DROP COLUMN IF EXISTS \"FotoPerfil\"; ALTER TABLE \"Usuarios\" ADD COLUMN \"FotoPerfil\" bytea;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert: drop bytea and recreate as text
            migrationBuilder.Sql("ALTER TABLE \"Usuarios\" DROP COLUMN IF EXISTS \"FotoPerfil\"; ALTER TABLE \"Usuarios\" ADD COLUMN \"FotoPerfil\" text;");
        }
    }
}
