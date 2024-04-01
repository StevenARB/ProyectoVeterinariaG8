using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ProyectoVeterinariaG8.DAL;

namespace ProyectoVeterinariaG8.Models
{
    public class MascotaImagenCreateViewModel
    {
        [Key]
        [DisplayName("Código de Imagen")]
        public int ImagenId { get; set; }

        [Required]
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }
    }
}
