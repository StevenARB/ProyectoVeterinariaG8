using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVeterinariaG8.DAL
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [MaxLength(100)]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [MaxLength(100)]
        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Display(Name = "Imagen del Usuario")]
        public byte[] Imagen { get; set; }

        [Display(Name = "Última Conexión")]
        public DateTime FechaUltimaConexion { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int EstadoUsuarioId { get; set; }

        public EstadoUsuario? EstadoUsuario { get; set; }
    }
}
