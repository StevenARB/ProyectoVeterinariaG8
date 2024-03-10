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
    [Table("MascotasImagenes")]
    public class MascotaImagen
    {
        [Key]
        [DisplayName("Código de Imagen")]
        public int ImagenId { get; set; }

        [Required]
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [DisplayName("Imagen de la Mascota")]
        public string Imagen { get; set; }

        public Mascota? Mascota { get; set; }
    }
}
