using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models
{
    public class CurrencyResponse
    {
        public string @base { get; set; }
        public string disclaimer { get; set; }
        public string license { get; set; }
        public rates rates { get; set; }
        public long timestamp { get; set; }
    }

    public class rates
    {
        public double DOP { get; set; }
    }
}