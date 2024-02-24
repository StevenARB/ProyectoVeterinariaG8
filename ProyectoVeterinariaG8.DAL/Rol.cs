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
    [Table("Roles")]
    public class Rol
    {
        [Key]
        [DisplayName("Código de Rol")]
        public int CodigoRol { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
    }
}
