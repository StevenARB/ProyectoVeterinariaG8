using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UsuariosIdentityMascota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCreacionId",
                table: "Mascotas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioModificacionId",
                table: "Mascotas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioPropietarioId",
                table: "Mascotas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioModificacionId",
                table: "Mascotas",
                column: "UsuarioModificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioPropietarioId",
                table: "Mascotas",
                column: "UsuarioPropietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioModificacionId",
                table: "Mascotas",
                column: "UsuarioModificacionId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioPropietarioId",
                table: "Mascotas",
                column: "UsuarioPropietarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_AspNetUsers_UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropIndex(
                name: "IX_Mascotas_UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioCreacionId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioModificacionId",
                table: "Mascotas");

            migrationBuilder.DropColumn(
                name: "UsuarioPropietarioId",
                table: "Mascotas");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioModificacionId",
                table: "Mascotas",
                column: "UsuarioModificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_UsuarioPropietarioId",
                table: "Mascotas",
                column: "UsuarioPropietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioCreacionId",
                table: "Mascotas",
                column: "UsuarioCreacionId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioModificacionId",
                table: "Mascotas",
                column: "UsuarioModificacionId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Usuarios_UsuarioPropietarioId",
                table: "Mascotas",
                column: "UsuarioPropietarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
