using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class MascotasVacunas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "MascotasVacunas",
                newName: "Tipo");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "MascotasVacunas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Producto",
                table: "MascotasVacunas",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "MascotasVacunas");

            migrationBuilder.DropColumn(
                name: "Producto",
                table: "MascotasVacunas");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "MascotasVacunas",
                newName: "Descripcion");
        }
    }
}
