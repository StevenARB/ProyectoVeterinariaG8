using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnStringImagen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "MascotasImagenes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "MascotasImagenes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
