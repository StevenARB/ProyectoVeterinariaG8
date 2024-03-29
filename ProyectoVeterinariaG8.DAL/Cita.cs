﻿using System;
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

        [ForeignKey("Mascota")]
        public int MascotaId { get; set; }

        [Required]
        [DisplayName("Fecha y Hora de la Cita")]
        public DateTime FechayHora { get; set; }

        [Required]
        [ForeignKey("CodigoUsuario")]
        [DisplayName("Veterinario Principal")]
        public int VeterinarioId1 { get; set; }

        [Required]
        [ForeignKey("CodigoUsuario")]
        [DisplayName("Veterinario Secundario")]
        public int VeterinarioId2 { get; set; }

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

        public Mascota? Mascota { get; set; }

        public Usuario? Usuario { get; set; }

        public Medicamento? Medicamento { get; set; }

        public EstadoCita? EstadoCita { get; set; }

    }
}
