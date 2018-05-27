using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaynyBatista.Models.ViewModels
{
    public class EditarProductoViewModel
    {
        public Producto Producto { get; set; }
        public List<Categoria> Categorias { get; set; }
    }
}