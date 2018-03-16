using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class ActualizarCitaViewModel
    {
        public int IdCita { get; set; }
        public int IdEstadoCita { get; set; }

        public DateTime Fecha { get; set; }

        public string HoraInicio { get; set; }
        
        public string HoraFin { get; set; }
    }
}