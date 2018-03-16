using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class EnviarMailViewModel
    {
        public string Id { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}