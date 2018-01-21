using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Models;

namespace HaynyBatista.Controllers
{
    public class ArticuloController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Articulo
        public ActionResult Index()
        {
            List<Articulo> articulos = db.Articulo.ToList();
            return View(articulos);
        }
    }
}