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
        [MaxLength(300)]
        [DisplayName("Raza de Mascota")]
        public string Descripcion { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
