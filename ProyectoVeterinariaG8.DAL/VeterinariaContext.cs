using Microsoft.EntityFrameworkCore;

namespace ProyectoVeterinariaG8.DAL
{
    public class VeterinariaContext : DbContext
    {
        public VeterinariaContext() { }

        public VeterinariaContext(DbContextOptions<VeterinariaContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

        public virtual DbSet<Usuario> Usuarios { get; set; }

        public virtual DbSet<EstadoUsuario> EstadosUsuario { get; set; }

        public virtual DbSet<Rol> Roles { get; set; }

        public virtual DbSet<Cita> Citas { get; set; }

        public virtual DbSet<Medicamento> Medicamentos { get; set; }

        public virtual DbSet<EstadoCita> EstadosCita { get; set; }

        public virtual DbSet<Mascota> Mascotas { get; set; }

        public virtual DbSet<MascotaPadecimiento> MascotasPadecimientos { get; set; }

        public virtual DbSet<MascotaVacuna> MascotasVacunas { get; set; }

        public virtual DbSet<MascotaImagen> MascotasImagenes { get; set; }

        public virtual DbSet<TipoMascota> TiposMascotas { get; set; }

        public virtual DbSet<RazaMascota> RazasMascotas { get; set; }
    }
}
