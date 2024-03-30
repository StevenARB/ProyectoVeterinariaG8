using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVeterinariaG8.DAL
{
    [Table("RazasMascotas")]
    public class RazaMascota
    {
        [Key]
        public int RazaId { get; set; }

        [Required]
        [ForeignKey("TipoMascota")]
        [DisplayName("Tipo de Mascota")]
        public int TipoId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Raza de Mascota")]
        public string Descripcion { get; set; }

        [DisplayName("Tipo de Mascota")]
        public TipoMascota? TipoMascota { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
