using HaynyBatista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Controllers
{
    public class ProductoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Producto
        public ViewResult List()
        {
            return View();
        }
    }
}