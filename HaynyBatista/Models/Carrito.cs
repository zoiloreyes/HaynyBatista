using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HaynyBatista.Models
{
    public class Carrito
    {
        private List<LineaCarrito> lineas = new List<LineaCarrito>();

        public int CantidadEnvioGratis { get { return 4000; } }

        public void AddItem(Producto producto, int cantidad)
        {
            LineaCarrito linea = lineas.Where(p => p.Producto.ProductoID == producto.ProductoID).FirstOrDefault();
            if(linea == null)
            {
                lineas.Add(new LineaCarrito { Producto = producto, Cantidad = cantidad });
            }
            else
            {
                linea.Cantidad += cantidad;
            }
        }
        
        public void RemoveLine(Producto product)
        {
            lineas.RemoveAll(l => l.Producto.ProductoID == product.ProductoID);
        }

        public decimal ComputeTotalValue()
        {
            return lineas.Sum(x => x.Producto.Precio * x.Cantidad);
        }

        public bool EnvioGratisAplicable()
        {
            return ComputeTotalValue() > CantidadEnvioGratis;
        }

        public void Clear()
        {
            lineas.Clear();
        }

        public IEnumerable<LineaCarrito> Lineas { get { return lineas; } }

    }
}