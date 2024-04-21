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
    [Table("MascotasVacunas")]
    public class MascotaVacuna
    {
        [Key]
        public int VacunaId { get; set; }

        [Required]
        [ForeignKey("Mascota")]
        [DisplayName("Código de Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [Required]
        [DisplayName("Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Producto")]
        public string Producto { get; set; }

        [DisplayName("Mascota")]
        public Mascota? Mascota { get; set; }
    }
}
