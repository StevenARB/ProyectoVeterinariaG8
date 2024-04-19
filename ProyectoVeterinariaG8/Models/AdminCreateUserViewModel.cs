using System.ComponentModel.DataAnnotations;

namespace ProyectoVeterinariaG8.Models
{
    public class AdminCreateUserViewModel
    {
        [Required(ErrorMessage = "El campo de nombre es requerido.")]
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

        [Display(Name = "Rol")]
        public string IdRol { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser como mínimo {2} y máximo {1} carácteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña de Confirmación")]
        [Compare("Password", ErrorMessage = "La contraseña no coincide con la contraseña de confirmación.")]
        public string ConfirmPassword { get; set; }
    }
}
