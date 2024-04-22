using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioIdentityCita : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_PrimerVeterinarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_SegundoVeterinarioId",
                table: "Citas");

            migrationBuilder.AlterColumn<string>(
                name: "SegundoVeterinarioId",
                table: "Citas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PrimerVeterinarioId",
                table: "Citas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_PrimerVeterinarioId",
                table: "Citas",
                column: "PrimerVeterinarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_AspNetUsers_SegundoVeterinarioId",
                table: "Citas",
                column: "SegundoVeterinarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_PrimerVeterinarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_AspNetUsers_SegundoVeterinarioId",
                table: "Citas");

            migrationBuilder.AlterColumn<int>(
                name: "SegundoVeterinarioId",
                table: "Citas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "PrimerVeterinarioId",
                table: "Citas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_PrimerVeterinarioId",
                table: "Citas",
                column: "PrimerVeterinarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_SegundoVeterinarioId",
                table: "Citas",
                column: "SegundoVeterinarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
