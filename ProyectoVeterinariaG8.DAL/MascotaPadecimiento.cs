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
    [Table("MascotasPadecimientos")]
    public class MascotaPadecimiento
    {
        [Key]
        public int PadecimientoId { get; set; }

        [Required]
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Descripción de Padecimiento")]
        public string Descripcion { get; set; }

        public Mascota? Mascota { get; set; }
    }
}
