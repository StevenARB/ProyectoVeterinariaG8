using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CitasVeterinarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VeterinarioId2",
                table: "Citas",
                newName: "SegundoVeterinarioId");

            migrationBuilder.RenameColumn(
                name: "VeterinarioId1",
                table: "Citas",
                newName: "PrimerVeterinarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PrimerVeterinarioId",
                table: "Citas",
                column: "PrimerVeterinarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_SegundoVeterinarioId",
                table: "Citas",
                column: "SegundoVeterinarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_PrimerVeterinarioId",
                table: "Citas",
                column: "PrimerVeterinarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_SegundoVeterinarioId",
                table: "Citas",
                column: "SegundoVeterinarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_PrimerVeterinarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_SegundoVeterinarioId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PrimerVeterinarioId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_SegundoVeterinarioId",
                table: "Citas");

            migrationBuilder.RenameColumn(
                name: "SegundoVeterinarioId",
                table: "Citas",
                newName: "VeterinarioId2");

            migrationBuilder.RenameColumn(
                name: "PrimerVeterinarioId",
                table: "Citas",
                newName: "VeterinarioId1");
        }
    }
}
