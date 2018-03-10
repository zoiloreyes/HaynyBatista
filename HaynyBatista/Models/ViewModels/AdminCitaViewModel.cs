using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class AdminCitaViewModel
    {
        
        public List<Cita> Citas { get; set; }
        public List<EstadoCita> Estados { get; set; }
    }
}