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
    public class Cita
    {
        [Key]
        public int CitaId { get; set; }

        [DisplayName("Mascota")]
        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [DisplayName("Fecha")]
        public DateTime FechayHora { get; set; }

        [Required]
        [ForeignKey(nameof(PrimerVeterinario))]
        [DisplayName("Código Veterinario 1")]
        public int PrimerVeterinarioId { get; set; }

        [Required]
        [ForeignKey(nameof(SegundoVeterinario))]
        [DisplayName("Código Veterinario 2")]
        public int SegundoVeterinarioId { get; set; }

        [Required]
        [DisplayName("Descripción")]
        [MaxLength(500)]
        public string DescripcionCita { get; set; }

        [DisplayName("Diagnóstico")]
        [MaxLength(200)]
        public string DiagnosticoCita { get; set; }

        [ForeignKey("Medicamento")]
        [DisplayName("Medicamentos")]
        public int MedicamentoId { get; set; }

        [Required]
        [ForeignKey("EstadoCita")]
        public int EstadoCitaId { get; set; }

        public Mascota? Mascota { get; set; }

        [DisplayName("Veterinario Principal")]
        public Usuario? PrimerVeterinario { get; set; }

        [DisplayName("Veterinario Secundario")]

        public Usuario? SegundoVeterinario { get; set; }

        public Medicamento? Medicamento { get; set; }

        [DisplayName("Estado")]

        public EstadoCita? EstadoCita { get; set; }

    }
}
