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
    [Table("Citas")]
    public class Citas
    {
        [Key]
        public int CitaId { get; set; }

        [ForeignKey("Mascotas")]
        public int MascotaId { get; set; }

        [Required]
        [DisplayName("Fecha y Hora de la Cita")]
        public DateTime FechayHora { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [DisplayName("Veterinario Principal")]
        public int CodigoUsuario { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        [DisplayName("Veterinario Secundario")]
         public int CodigoUsuario2 { get; set; }

        [Required]
        [DisplayName("Descripción de la Cita")]
        [MaxLength(500)]
        public string DescripcionCita { get; set; }

        [DisplayName("Diagnóstico de la Cita")]
        [MaxLength(200)]
        public string DiagnosticoCita { get; set; }


        [ForeignKey("Medicamento")]
        [DisplayName("Medicamentos")]
        public int MedicamentoId { get; set; }

        [Required]
        [ForeignKey("EstadoCita")]
        public int EstadoCitaId { get; set; }


        public Usuario? Usuario { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
