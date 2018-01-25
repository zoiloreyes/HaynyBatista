using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using HaynyBatista.Extensions;
using System.IO;
using HaynyBatista.UtilClases;

namespace HaynyBatista.Controllers
{
    public class ArticuloController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Articulo
        public ActionResult Index()
        {
            List<Articulo> articulos = db.Articulos.ToList();
            return View(articulos);
        }

        [HttpPost]
        public ActionResult GuardarArticulo(GuardarArticuloViewModel model)
        {
            Retorno retorno = new Retorno() { Success = true, Message = "Articulo añadido correctamente" };
            var user = db.Users.Find(User.Identity.GetUserId());
            var usuarioHayny = db.Usuarios.Find(user.Usuario);
            List<Etiqueta> etiquetas = JsonConvert.DeserializeObject<List<Etiqueta>>(model.Etiquetas);

            Imagen imagenArticulo;


            if(Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);
                if (HttpPostedFileBaseExtensions.IsImage(file))
                {

                    Stream stream = file.InputStream;
                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream)
                    imagenArticulo = new Imagen()
                    {
                        Usuario = usuarioHayny,
                        FechaSubida = DateTime.Now,
                        Height = image.Height,
                        Width = image.Width,


                    };
                }
                else
                {
                    retorno = new Retorno() { Success = false, Message = "El archivo subido no es una imagen" };
                    return Json(retorno, JsonRequestBehavior.AllowGet);
                }
            }
            Articulo articulo = new Articulo() { }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}