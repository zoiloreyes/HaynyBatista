﻿using System;
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
using HaynyBatista.UtilClasses;
using System.Data.Entity.Validation;
using PagedList;

namespace HaynyBatista.Controllers
{
    public class ArticuloController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Detalle(int? id)
        {
            Articulo articulo = db.Articulos.Find(id);

            if (articulo == null)
            {
                return HttpNotFound();
            }
            articulo.Visitas++;
            db.Entry(articulo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return View(articulo);

        }
        
        // GET: Articulo
        public ActionResult Index(int? idUsuario, int? page, string filtro)
        {
            List<Articulo> articulos = db.Articulos.OrderByDescending(x => x.FechaSubida).ToList();

            if (idUsuario != null)
            {
                articulos = articulos.Where(x => x.Usuario.IdUsuario == idUsuario).ToList();
            }

            if(!String.IsNullOrWhiteSpace(filtro))
            {
                ViewBag.Filtro = filtro;
                articulos =
                (from articulo in db.Articulos
                 join articuloEtiqueta in db.EtiquetaArticulo on articulo.IdArticulo equals articuloEtiqueta.IdArticulo
                 join etiquetaP in db.Etiquetas on articuloEtiqueta.IdEtiqueta equals etiquetaP.IdEtiqueta
                 where etiquetaP.Nombre.Contains(filtro) || articulo.Titulo.Contains(filtro)
                 select articulo).Distinct().OrderByDescending(x => x.FechaSubida).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(articulos.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult Filter(string[] parametros, int? page)
        {
            var articulos =
                (from articulo in db.Articulos
                 join articuloEtiqueta in db.EtiquetaArticulo on articulo.IdArticulo equals articuloEtiqueta.IdArticulo
                 join etiquetaP in db.Etiquetas on articuloEtiqueta.IdEtiqueta equals etiquetaP.IdEtiqueta
                 where parametros.Contains(etiquetaP.Nombre) || parametros.Contains(articulo.Titulo)
                select articulo).Distinct().OrderByDescending(s => s.FechaSubida);

            return View(articulos);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult BorrarArticulo(int? id)
        {
            Retorno retorno = new Retorno() { Success = true, Message = "Articulo Borrado correctamente" };
            var user = db.Users.Find(User.Identity.GetUserId());
            var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
            Articulo articulo = db.Articulos.Find(id);
            Imagen imagenArticulo = articulo.Imagen;

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    
                        if (articulo.Imagen != null)
                        {

                            string path = Path.Combine(Server.MapPath("~/ImagenesSubidas/"), articulo.Imagen.IdImagen + articulo.Imagen.Formato);

                            try
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                    db.Imagenes.Remove(articulo.Imagen);
                                }
                            }
                            catch (Exception e)
                            {
                                LogFileCreator LogError = new LogFileCreator();
                                LogError.ErrorLog(Server.MapPath("~/Logs"), e.Message);
                                retorno = new Retorno() { Success = false, Message = "Ocurrió un error al subir el archivo" };
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                    
                    foreach (EtiquetaArticulo e in articulo.EtiquetaArticulo.ToList())
                    {
                        db.EtiquetaArticulo.Remove(e);
                    }
                    db.Articulos.Remove(articulo);
                    db.SaveChanges();

                    dbTransaction.Commit();
                }
                catch (DbEntityValidationException e)
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
                catch (Exception e)
                {
                    LogFileCreator LogError = new LogFileCreator();
                    LogError.ErrorLog(Server.MapPath("/Logs"), e.Message);
                    dbTransaction.Rollback();
                }



            }


            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult ActualizarArticulo(ActualizarArticuloViewModel model)
        {
            Retorno retorno = new Retorno() { Success = true, Message = "Articulo actualizado correctamente" };
            var user = db.Users.Find(User.Identity.GetUserId());
            var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
            Articulo articulo = db.Articulos.Find(model.IdArticulo);
            List<Etiqueta> etiquetas = JsonConvert.DeserializeObject<List<Etiqueta>>(model.Etiquetas);
            Imagen imagenArticulo = articulo.Imagen;

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    
                    if (Request.Files.Count > 0)
                    {
                        if (articulo.Imagen != null)
                        {
                            
                            string path = Path.Combine(Server.MapPath("~/ImagenesSubidas/"), articulo.Imagen.IdImagen + articulo.Imagen.Formato);
                            
                            try
                            {
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Delete(path);
                                    db.Imagenes.Remove(articulo.Imagen);
                                }
                            }
                            catch (Exception e)
                            {
                                LogFileCreator LogError = new LogFileCreator();
                                LogError.ErrorLog(Server.MapPath("/Logs"), e.Message);
                                retorno = new Retorno() { Success = false, Message = "Ocurrió un error al subir el archivo" };
                                return Json(retorno, JsonRequestBehavior.AllowGet);
                            }
                        }
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var fileExtension = Path.GetExtension(file.FileName);
                        if (HttpPostedFileBaseExtensions.IsImage(file))
                        {
                            try
                            {
                                Stream stream = file.InputStream;
                                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                                imagenArticulo = new Imagen()
                                {
                                    Usuario = usuarioHayny,
                                    FechaSubida = DateTime.Now,
                                    Height = image.Height,
                                    Width = image.Width,
                                    Formato = fileExtension,
                                    Title = fileName
                                };
                                db.Imagenes.Add(imagenArticulo);
                                db.SaveChanges();
                                string path = Path.Combine(Server.MapPath("~/ImagenesSubidas/"), imagenArticulo.IdImagen + fileExtension);
                                file.SaveAs(path);
                            }
                            catch (Exception e)
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

                    
                    articulo.Titulo = model.Titulo;
                    articulo.Contenido = model.Contenido;
                    articulo.Imagen = imagenArticulo;
                    db.SaveChanges();
                    foreach(EtiquetaArticulo e in articulo.EtiquetaArticulo.ToList())
                    {
                        db.EtiquetaArticulo.Remove(e);
                    }
                    db.SaveChanges();
                    foreach (Etiqueta etiq in etiquetas)
                    {
                        etiq.Nombre = etiq.Nombre.ToLowerInvariant();
                        db.Etiquetas.Add(etiq);
                        EtiquetaArticulo etar = new EtiquetaArticulo() { Articulo = articulo, Etiqueta = etiq };
                        db.EtiquetaArticulo.Add(etar);
                    }
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (DbEntityValidationException e)
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
                catch (Exception e)
                {
                    LogFileCreator LogError = new LogFileCreator();
                    LogError.ErrorLog(Server.MapPath("/Logs"), e.Message);
                    dbTransaction.Rollback();
                }



            }


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult GuardarArticulo(GuardarArticuloViewModel model)
        {
            Retorno retorno = new Retorno() { Success = true, Message = "Articulo añadido correctamente" };
            var user = db.Users.Find(User.Identity.GetUserId());
            var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
            List<Etiqueta> etiquetas = JsonConvert.DeserializeObject<List<Etiqueta>>(model.Etiquetas);
            Imagen imagenArticulo = null;

            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        var fileExtension = Path.GetExtension(file.FileName);
                        if (HttpPostedFileBaseExtensions.IsImage(file))
                        {
                            try
                            {
                                Stream stream = file.InputStream;
                                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                                imagenArticulo = new Imagen()
                                {
                                    Usuario = usuarioHayny,
                                    FechaSubida = DateTime.Now,
                                    Height = image.Height,
                                    Width = image.Width,
                                    Formato = fileExtension,
                                    Title = fileName
                                };
                                db.Imagenes.Add(imagenArticulo);
                                db.SaveChanges();
                                string path = Path.Combine(Server.MapPath("~/ImagenesSubidas/"), imagenArticulo.IdImagen + fileExtension);
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

                    
                    Articulo articulo = new Articulo()
                    {
                        Titulo = model.Titulo,
                        Contenido = model.Contenido,
                        FechaSubida = DateTime.Now,
                        Usuario = usuarioHayny,
                        Imagen = imagenArticulo
                    };
                    db.Articulos.Add(articulo);
                        foreach (Etiqueta etiq in etiquetas)
                    {
                        etiq.Nombre = etiq.Nombre.ToLowerInvariant();
                        db.Etiquetas.Add(etiq);
                        EtiquetaArticulo etar = new EtiquetaArticulo() { Articulo = articulo, Etiqueta = etiq };
                        db.EtiquetaArticulo.Add(etar);
                    }
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (DbEntityValidationException e)
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
                catch (Exception e)
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