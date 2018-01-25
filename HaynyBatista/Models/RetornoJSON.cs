using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class Retorno
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Object Object { get; set; }
    }
}