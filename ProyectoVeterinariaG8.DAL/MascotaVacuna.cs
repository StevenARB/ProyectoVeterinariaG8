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
        public int MascotaId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Descripción de Vacuna")]
        public string Descripcion { get; set; }

        public Mascota? Mascota { get; set; }
    }
}
