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
    [Table("EstadosMascotas")]
    public class EstadoMascota
    {
        [Key]
        [DisplayName("Código de Estado")]
        public int EstadoId { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();
    }
}
