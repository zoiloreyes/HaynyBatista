using HaynyBatista.Models;
using HaynyBatista.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Controllers
{
    public class ProductoController : Controller
    {
        public int PageSize = 5;
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Producto
        public ViewResult List(int page=1)
        {
            ListaProductosViewModel model = new ListaProductosViewModel
            {
                Productos = db.Productos.OrderBy(p => p.ProductoID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = db.Productos.Count()
                }
            };

            return View(model);
        }
    }
}