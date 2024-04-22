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

        [Display(Name = "Estado")]
        public EstadoUsuario? EstadoUsuario { get; set; }

        [InverseProperty(nameof(Mascota.UsuarioPropietario))]
        public ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

        [InverseProperty(nameof(Mascota.UsuarioCreacion))]
        public ICollection<Mascota> MascotasCreadas { get; set; } = new List<Mascota>();

        [InverseProperty(nameof(Mascota.UsuarioModificacion))]
        public ICollection<Mascota> MascotasModificadas { get; set; } = new List<Mascota>();

        [InverseProperty(nameof(Cita.PrimerVeterinario))]
        public ICollection<Cita> Veterinarios1 { get; set; } = new List<Cita>();

        [InverseProperty(nameof(Cita.SegundoVeterinario))]
        public ICollection<Cita> Veterinarios2 { get; set; } = new List<Cita>();
    }
}
