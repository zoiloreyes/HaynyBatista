using HaynyBatista.Models;
using HaynyBatista.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Controllers
{
    public class CarritoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ViewResult Index(Carrito carrito, string returnUrl)
        {
            return View(new CartIndexViewModel { ReturnUrl = returnUrl, Carrito = carrito });
        }
        public RedirectToRouteResult AddToCart(Carrito carrito, int productId, int quantity, string returnUrl)
        {
            Producto product = db.Productos
            .FirstOrDefault(p => p.ProductoID == productId);
            if (product != null)
            {
                carrito.AddItem(product, quantity);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult UpdateCartQuantity(Carrito carrito, int productId, int quantity)
        {

            LineaCarrito linea = carrito.Lineas.Where(l => l.Producto.ProductoID == productId).FirstOrDefault();
            if(linea != null)
            {
                linea.Cantidad = quantity;
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult Summary(Carrito carrito)
        {
            return PartialView(carrito);
        }
        
        public RedirectToRouteResult RemoveFromCart(Carrito carrito, int productId, string returnUrl)
        {
            Producto product = db.Productos
            .FirstOrDefault(p => p.ProductoID == productId);
            if (product != null)
            {
                carrito.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        
    }
}