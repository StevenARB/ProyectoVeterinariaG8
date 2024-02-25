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
        public int CodigoMascota { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        
        [Required]
        [ForeignKey("TipoMascota")]
        public string Tipo { get; set; }

        [Required]
        [ForeignKey("RazaMascota")]
        public string Raza { get; set; }

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
        public string Propietario { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int CodigoUsuarioCreacion { get; set; }

        [ForeignKey("Usuario")]
        public int CodigoUsuarioModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
