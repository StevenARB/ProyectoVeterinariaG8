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
    [Table("TiposMascotas")]
    public class TipoMascota
    {
        [Key]
        [DisplayName("Código Tipo de Mascota")]
        public int TipoId { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Tipo de Mascota")]
        public string Descripcion { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

        public ICollection<RazaMascota> RazasMascota { get; set; } = new List<RazaMascota>();
    }
}
