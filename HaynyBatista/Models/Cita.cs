using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class Cita
    {
        [Key]
        public int IdCita { get; set; }

        //public int? IdUsuario { get; set; }

        public int? IdEstadoCita { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFin { get; set; }

        [Required]
        public double Costo { get; set; }

        public string Mensaje { get; set; }

        public int? IdTipoCita { get; set; }

        [ForeignKey("FormaPago")]
        public int IdFormaPago { get; set; }

        public virtual TipoCita TipoCita { get; set; }
        public virtual EstadoCita EstadoCita { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual FormaPago FormaPago { get; set; }
    }
}