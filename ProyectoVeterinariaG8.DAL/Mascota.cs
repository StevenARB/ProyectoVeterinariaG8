using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVeterinariaG8.DAL
{
    [Table("Mascotas")]
    public class Mascota
    {
        [Key]
        [DisplayName("Código Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [ForeignKey("TipoMascota")]
        [DisplayName("Tipo")]
        public int TipoId { get; set; }

        [Required]
        [ForeignKey("RazaMascota")]
        [DisplayName("Raza")]
        public int RazaId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Género")]
        public string Genero { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public double Peso { get; set; }

        [Required]
        [ForeignKey("EstadoMascota")]
        [DisplayName("Estado")]
        public int EstadoId { get; set; }

        [Required]
        [DisplayName("Dueño")]
        public string UsuarioPropietarioId { get; set; }

        [Required]
        [DisplayName("Usuario Creación")]
        public string UsuarioCreacionId { get; set; }

        [DisplayName("Usuario Modificación")]
        public string? UsuarioModificacionId { get; set; }

        [Required]
        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [DisplayName("Fecha de Modificación")]
        public DateTime? FechaModificacion { get; set; }

        [DisplayName("Tipo")]
        public TipoMascota? TipoMascota { get; set; }

        [DisplayName("Raza")]
        public RazaMascota? RazaMascota { get; set; }

        [DisplayName("Estado")]
        public EstadoMascota? EstadoMascota { get; set; }

        public ApplicationUser? UsuarioPropietario { get; set; }

        public ApplicationUser? UsuarioCreacion { get; set; }

        public ApplicationUser? UsuarioModificacion { get; set; }

        public ICollection<MascotaPadecimiento> MascotaPadecimientos { get; set; } = new List<MascotaPadecimiento>();
        
        public ICollection<MascotaVacuna> MascotaVacunas{ get; set; } = new List<MascotaVacuna>();

        public ICollection<MascotaImagen> MascotaImagenes { get; set; } = new List<MascotaImagen>();

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
