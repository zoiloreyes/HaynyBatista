using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class ListaProductosViewModel
    {
        public IEnumerable<Producto> Productos { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}