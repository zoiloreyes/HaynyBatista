using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Infrastructure.Attributes;
using HaynyBatista.Models;
using HaynyBatista.Extensions;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Data.Entity;
using HaynyBatista.UtilClasses;

namespace HaynyBatista.Controllers
{
    [LoginRedirectAuthorize]
    public class CompraController : Controller
    {
        [HttpGet]
        public ActionResult Create(Carrito carrito)
        {
            if (!carrito.Lineas.Any())
            {
                return RedirectToAction("List", "Producto");
            }
            Compra Compra = new Compra
            {
                IdUsuario = Int32.Parse(User.Identity.GetHaynyUserID()),
                ItemsCompra = new List<ItemCompra>()
            };

            foreach (LineaCarrito linea in carrito.Lineas)
            {
                Compra.ItemsCompra.Add(new ItemCompra
                {
                    Cantidad = linea.Cantidad,
                    Producto = linea.Producto,
                    PrecioTotal = linea.Producto.Precio * linea.Cantidad,
                    ProductoID = linea.Producto.ProductoID
                });
            }
            try
            {
                //Get current exchange rate of USD
                WebRequest request = WebRequest.Create("https://openexchangerates.org/api/latest.json?app_id=ac7c178cc4d14703b579f6fcc49410b2&base=USD&symbols=DOP");
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = 30000;
                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        CurrencyResponse currencyresponse = JsonConvert.DeserializeObject<CurrencyResponse>(reader.ReadToEnd());
                        double Costo = (double)carrito.ComputeTotalValue() / currencyresponse.rates.DOP;
                        ViewBag.TotalUSD = Math.Round(Costo,2);
                    }
                }

                return View(Compra);
            }
            catch (Exception e)
            {
                LogFileCreator LogError = new LogFileCreator();
                LogError.ErrorLog(Server.MapPath("~/Logs"), e.Message);
                return RedirectToAction("Index", "Home");
            }

           
        }

        [HttpPost]
        public ActionResult Create(Carrito carrito, Compra compra)
        {
            carrito.Clear();

            compra.FechaCreacion = DateTime.Now;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                foreach (ItemCompra item in compra.ItemsCompra)
                {
                    item.Compra = compra;
                    item.Producto = db.Productos.Find(item.ProductoID);
                    item.Producto.Cantidad -= item.Cantidad;
                }

                db.Compras.Add(compra);
                db.SaveChanges();
            }
            if (MailSender.SendEmailWithLogo("HaynyBatista@haynybatista.com", "@Hayny.Batista", User.Identity.GetEmail(), "Orden Completada " + DateTime.Now.ToString("dd/MM/yyyy"), FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/Orden.cshtml",compra, false)))
            {
                return RedirectToAction("Completada");
            }

                return RedirectToAction("Completada");

        }

        [HttpGet]
        public ActionResult Detalles(int CompraID) {
            using (ApplicationDbContext context = new ApplicationDbContext()) {
                Compra compra = context.Compras.Include(c => c.ItemsCompra.Select(i => i.Producto)).Include(c => c.Usuario).Where(c => c.CompraID == CompraID).FirstOrDefault();
                return View(compra);
            }
        }

        [HttpGet]
        public ActionResult Completada()
        {
            return View();
        }
    }
}