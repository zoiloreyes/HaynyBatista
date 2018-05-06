using HaynyBatista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Controllers
{
    public class NavigationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Navigation
        public PartialViewResult CategoryMenu(string currentCategory = null)
        {
            ViewBag.SelectedCategory = currentCategory;
            IEnumerable<string> categories = db.Categorias.Select(c => c.Nombre).OrderBy(c => c);
            return PartialView(categories);
        }
    }
}