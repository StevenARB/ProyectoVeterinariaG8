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
    [Table("TiposMascotas")]
    public class TipoMascota
    {
        [Key]
        public int CodigoTipo { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Tipo de Mascota")]
        public string Descripcion { get; set; }
    }
}
