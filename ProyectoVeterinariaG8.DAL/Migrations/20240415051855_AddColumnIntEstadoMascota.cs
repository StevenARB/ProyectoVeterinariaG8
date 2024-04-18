using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnIntEstadoMascota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Mascotas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadosMascotas",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosMascotas", x => x.EstadoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_EstadoId",
                table: "Mascotas",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_EstadosMascotas_EstadoId",
                table: "Mascotas",
                column: "EstadoId",
                principalTable: "EstadosMascotas",
                principalColumn: "EstadoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_EstadosMascotas_EstadoId",
                table: "Mascotas");

            migrationBuilder.DropTable(
                name: "EstadosMascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_EstadoId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Mascotas");
        }
    }
}
