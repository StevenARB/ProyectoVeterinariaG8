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
    [Table("EstadoCita")]
    public class EstadoCita
    {
        [Key]
        public int EstadoCitaId { get; set; }

        [Required]
        public string DescripcionCita { get; set; }
    }
}
