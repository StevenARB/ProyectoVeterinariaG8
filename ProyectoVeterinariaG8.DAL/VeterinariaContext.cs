using Microsoft.EntityFrameworkCore;

namespace ProyectoVeterinariaG8.DAL
{
    public class VeterinariaContext : DbContext
    {
        public VeterinariaContext() { }

        public VeterinariaContext(DbContextOptions<VeterinariaContext>)


      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

        public virtual DbSet<Citas> Citas { get; set; }

        public virtual DbSet<Medicamento> Medicamento { get; set;}

        public virtual DbSet<EstadoCita> EstadoCitas { get; set; }
    }
}
