using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaynyBatista.Models
{
    public class EstadoCita
    {
        [Key]
        public int IdEstadoCita { get; set; }
        
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Color { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
    }
}