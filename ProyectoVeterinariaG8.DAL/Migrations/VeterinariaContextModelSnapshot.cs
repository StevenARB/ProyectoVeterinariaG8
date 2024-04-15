﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoVeterinariaG8.DAL;

#nullable disable

namespace ProyectoVeterinariaG8.DAL.Migrations
{
    [DbContext(typeof(VeterinariaContext))]
    partial class VeterinariaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Cita", b =>
                {
                    b.Property<int>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CitaId"));

                    b.Property<string>("DescripcionCita")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DiagnosticoCita")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("EstadoCitaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechayHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.Property<int>("MedicamentoId")
                        .HasColumnType("int");

                    b.Property<int>("PrimerVeterinarioId")
                        .HasColumnType("int");

                    b.Property<int>("SegundoVeterinarioId")
                        .HasColumnType("int");

                    b.HasKey("CitaId");

                    b.HasIndex("EstadoCitaId");

                    b.HasIndex("MascotaId");

                    b.HasIndex("MedicamentoId");

                    b.HasIndex("PrimerVeterinarioId");

                    b.HasIndex("SegundoVeterinarioId");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.EstadoCita", b =>
                {
                    b.Property<int>("EstadoCitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstadoCitaId"));

                    b.Property<string>("DescripcionCita")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstadoCitaId");

                    b.ToTable("EstadosCita");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.EstadoMascota", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstadoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstadoId");

                    b.ToTable("EstadosMascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.EstadoUsuario", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EstadoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EstadoId");

                    b.ToTable("EstadosUsuario");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Mascota", b =>
                {
                    b.Property<int>("MascotaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MascotaId"));

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.Property<int>("RazaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioCreacionId")
                        .HasColumnType("int");

                    b.Property<int?>("UsuarioModificacionId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioPropietarioId")
                        .HasColumnType("int");

                    b.HasKey("MascotaId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("RazaId");

                    b.HasIndex("TipoId");

                    b.HasIndex("UsuarioCreacionId");

                    b.HasIndex("UsuarioModificacionId");

                    b.HasIndex("UsuarioPropietarioId");

                    b.ToTable("Mascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaImagen", b =>
                {
                    b.Property<int>("ImagenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImagenId"));

                    b.Property<byte[]>("Imagen")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.HasKey("ImagenId");

                    b.HasIndex("MascotaId");

                    b.ToTable("MascotasImagenes");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaPadecimiento", b =>
                {
                    b.Property<int>("PadecimientoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PadecimientoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.HasKey("PadecimientoId");

                    b.HasIndex("MascotaId");

                    b.ToTable("MascotasPadecimientos");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaVacuna", b =>
                {
                    b.Property<int>("VacunaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VacunaId"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("MascotaId")
                        .HasColumnType("int");

                    b.Property<string>("Producto")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("VacunaId");

                    b.HasIndex("MascotaId");

                    b.ToTable("MascotasVacunas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Medicamento", b =>
                {
                    b.Property<int>("MedicamentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicamentoId"));

                    b.Property<string>("Dosis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicamentoId");

                    b.ToTable("Medicamentos");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.RazaMascota", b =>
                {
                    b.Property<int>("RazaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RazaId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("RazaId");

                    b.HasIndex("TipoId");

                    b.ToTable("RazasMascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.TipoMascota", b =>
                {
                    b.Property<int>("TipoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TipoId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("TipoId");

                    b.ToTable("TiposMascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasenna")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.HasIndex("EstadoId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Cita", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.EstadoCita", "EstadoCita")
                        .WithMany()
                        .HasForeignKey("EstadoCitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Mascota", "Mascota")
                        .WithMany("Citas")
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Medicamento", "Medicamento")
                        .WithMany()
                        .HasForeignKey("MedicamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Usuario", "PrimerVeterinario")
                        .WithMany("Veterinarios1")
                        .HasForeignKey("PrimerVeterinarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Usuario", "SegundoVeterinario")
                        .WithMany("Veterinarios2")
                        .HasForeignKey("SegundoVeterinarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoCita");

                    b.Navigation("Mascota");

                    b.Navigation("Medicamento");

                    b.Navigation("PrimerVeterinario");

                    b.Navigation("SegundoVeterinario");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Mascota", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.EstadoMascota", "EstadoMascota")
                        .WithMany("Mascotas")
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.RazaMascota", "RazaMascota")
                        .WithMany("Mascotas")
                        .HasForeignKey("RazaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.TipoMascota", "TipoMascota")
                        .WithMany("Mascotas")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Usuario", "UsuarioCreacion")
                        .WithMany("MascotasCreadas")
                        .HasForeignKey("UsuarioCreacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Usuario", "UsuarioModificacion")
                        .WithMany("MascotasModificadas")
                        .HasForeignKey("UsuarioModificacionId");

                    b.HasOne("ProyectoVeterinariaG8.DAL.Usuario", "UsuarioPropietario")
                        .WithMany("Mascotas")
                        .HasForeignKey("UsuarioPropietarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoMascota");

                    b.Navigation("RazaMascota");

                    b.Navigation("TipoMascota");

                    b.Navigation("UsuarioCreacion");

                    b.Navigation("UsuarioModificacion");

                    b.Navigation("UsuarioPropietario");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaImagen", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.Mascota", "Mascota")
                        .WithMany("MascotaImagenes")
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mascota");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaPadecimiento", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.Mascota", "Mascota")
                        .WithMany("MascotaPadecimientos")
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mascota");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.MascotaVacuna", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.Mascota", "Mascota")
                        .WithMany("MascotaVacunas")
                        .HasForeignKey("MascotaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mascota");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.RazaMascota", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.TipoMascota", "TipoMascota")
                        .WithMany("RazasMascota")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoMascota");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Usuario", b =>
                {
                    b.HasOne("ProyectoVeterinariaG8.DAL.EstadoUsuario", "EstadoUsuario")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoVeterinariaG8.DAL.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoUsuario");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.EstadoMascota", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Mascota", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("MascotaImagenes");

                    b.Navigation("MascotaPadecimientos");

                    b.Navigation("MascotaVacunas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.RazaMascota", b =>
                {
                    b.Navigation("Mascotas");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.TipoMascota", b =>
                {
                    b.Navigation("Mascotas");

                    b.Navigation("RazasMascota");
                });

            modelBuilder.Entity("ProyectoVeterinariaG8.DAL.Usuario", b =>
                {
                    b.Navigation("Mascotas");

                    b.Navigation("MascotasCreadas");

                    b.Navigation("MascotasModificadas");

                    b.Navigation("Veterinarios1");

                    b.Navigation("Veterinarios2");
                });
#pragma warning restore 612, 618
        }
    }
}
