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
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DisplayName("Código de usuario")]
        public int UsuarioId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        [DisplayName("Contraseña")]
        public string Contrasenna { get; set; }

        [Required]
        [ForeignKey("Rol")]
        public int RolId { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        [ForeignKey("EstadoUsuario")]
        public int EstadoId { get; set; }

        [Required]
        [DisplayName("Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        public Rol? Rol { get; set; } 

        public EstadoUsuario? EstadoUsuario { get; set; }

        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    }
}
