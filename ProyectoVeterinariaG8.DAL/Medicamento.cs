using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoVeterinariaG8.DAL
{
    [Table("Medicamento")]
    public class Medicamento
    {
        [Key]
        public int MedicamentoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Dosis { get; set; }
    }
}
