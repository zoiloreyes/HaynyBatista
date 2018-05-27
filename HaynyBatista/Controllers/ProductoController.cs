using HaynyBatista.Models;
using HaynyBatista.Models.ViewModels;
using HaynyBatista.UtilClases;
using HaynyBatista.UtilClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaynyBatista.Controllers
{
    public class ProductoController : Controller
    {
        public int PageSize = 9;
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Producto
        public ViewResult List(string categoria, int page=1)
        {
            var Productos = db.Productos
                .Where(p => categoria == null || p.Categoria.Nombre == categoria)
                .OrderBy(p => p.ProductoID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            ListaProductosViewModel model = new ListaProductosViewModel
            {
                Productos = Productos,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = Productos.Count(),
                    CategoriaActual = categoria
                },
                Categorias = db.Categorias
            };
            
            return View(model);
        }
        [HttpGet]
        public ViewResult Detalle(int Id)
        {
            Producto producto = db.Productos.Find(Id);

            if(producto != null)
            {
                return View(producto);
            }
            else
            {
                return View("ErrorView", new ErrorViewModel { Summary = "Error 404", Description = "La página no existe" });
            }
        }

        [HttpGet]
        public ActionResult Editar(int id) {

            EditarProductoViewModel model = new EditarProductoViewModel
            {
                Producto = db.Productos.Find(id),
                Categorias = db.Categorias.ToList()
            };
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(Producto producto) {
            db.Entry(producto).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("List");
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult GuardarProducto(Producto p)
        {
            Retorno retorno = new Retorno() { Success = true, Message = "Producto añadido correctamente" };
            var user = db.Users.Find(User.Identity.GetUserId());
            var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
            Imagen imagenProducto = null;
            using(var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if(Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        string fileName = Path.GetFileName(file.FileName);
                        string fileExtension = Path.GetExtension(file.FileName);

                        if (HttpPostedFileBaseExtensions.IsImage(file))
                        {
                            try
                            {
                                Stream stream = file.InputStream;
                                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                                imagenProducto = new Imagen()
                                {
                                    Usuario = usuarioHayny,
                                    FechaSubida = DateTime.Now,
                                    Height = image.Height,
                                    Width = image.Width,
                                    Formato = fileExtension,
                                    Title = fileName
                                };
                                db.Imagenes.Add(imagenProducto);
                                db.SaveChanges();
                                string path = Path.Combine(Server.MapPath("~/ImagenesSubidas/"), imagenProducto.IdImagen + fileExtension);
                                file.SaveAs(path);
                            }
                            catch(Exception e)
                            {
                                LogFileCreator LogError = new LogFileCreator();
                                LogError.ErrorLog(Server.MapPath("/Logs"), e.Message);
                                retorno = new Retorno() { Success = false, Message = "Ocurrió un error al subir el archivo" };
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            retorno = new Retorno() { Success = false, Message = "El archivo subido no es una imagen válida" };
                            return Json(retorno, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        retorno.Success = false;
                        retorno.Message = "Debe incluir una imagen para agregar el producto";
                    }

                    p.Imagen = imagenProducto;
                    p.FechaCreacion = DateTime.Now;
                    db.Productos.Add(p);
                    db.SaveChanges();
                    dbTransaction.Commit();

                }
                catch(DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        LogFileCreator LogError = new LogFileCreator();

                        foreach (var ve in eve.ValidationErrors)
                        {
                            var mensaje = String.Format("  \"{0}\"  , {1} , {2}", eve.Entry.Entity.GetType().Name, ve.PropertyName, ve.ErrorMessage);
                            LogError.ErrorLog(Server.MapPath("/Logs"), mensaje);
                        }
                    }
                }
                catch(Exception e)
                {
                    LogFileCreator LogError = new LogFileCreator();
                    LogError.ErrorLog(Server.MapPath("/Logs"), e.Message);
                    dbTransaction.Rollback();
                }


                
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}