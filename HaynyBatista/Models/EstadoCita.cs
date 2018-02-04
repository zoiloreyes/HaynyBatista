using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaynyBatista.Models
{
    public class EstadoCita
    {
        [Key]
        public int IdEstadoCita { get; set; }

        public string Nombre { get; set; }
        public virtual ICollection<Cita> Cita { get; set; }
    }
}