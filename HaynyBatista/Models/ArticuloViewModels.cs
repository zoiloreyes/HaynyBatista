using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Models
{
    public class GuardarArticuloViewModel
    {
        public string Titulo { get; set; }
        [AllowHtml]
        public string Contenido { get; set; }

        public virtual string Etiquetas { get; set; }
    }
}