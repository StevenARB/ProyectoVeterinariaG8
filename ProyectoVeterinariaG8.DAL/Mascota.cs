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
        [DisplayName("Código de Mascota")]
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
        [ForeignKey("Usuario")]
        [DisplayName("Dueño")]
        public int PropietarioId { get; set; }

        [Required]
        public int UsuarioCreacionId { get; set; }

        public int? UsuarioModificacionId { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [DisplayName("Tipo")]
        public TipoMascota? TipoMascota { get; set; }

        [DisplayName("Raza")]
        public RazaMascota? RazaMascota { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<MascotaPadecimiento> MascotaPadecimientos { get; set; } = new List<MascotaPadecimiento>();
        
        public ICollection<MascotaVacuna> MascotaVacunas{ get; set; } = new List<MascotaVacuna>();

        public ICollection<MascotaImagen> MascotaImagenes { get; set; } = new List<MascotaImagen>();
    }
}
