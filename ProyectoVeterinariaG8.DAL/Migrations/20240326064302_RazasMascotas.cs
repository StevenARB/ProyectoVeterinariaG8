using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RazasMascotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "RazasMascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DiagnosticoCita",
                table: "Citas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_RazasMascotas_TipoId",
                table: "RazasMascotas",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RazasMascotas_TiposMascotas_TipoId",
                table: "RazasMascotas",
                column: "TipoId",
                principalTable: "TiposMascotas",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RazasMascotas_TiposMascotas_TipoId",
                table: "RazasMascotas");

            migrationBuilder.DropIndex(
                name: "IX_RazasMascotas_TipoId",
                table: "RazasMascotas");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "RazasMascotas");

            migrationBuilder.AlterColumn<string>(
                name: "DiagnosticoCita",
                table: "Citas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
