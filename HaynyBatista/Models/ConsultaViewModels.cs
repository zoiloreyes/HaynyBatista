using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class IndiceConsulta {
        public List<Cita> Citas { get; set; }
        public List<TipoCita> TiposCita { get; set; }

    }

    public class NuevaCitaViewModel
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public String Mensaje { get; set; }
        public int IdTipoCita { get; set; }
    }
}