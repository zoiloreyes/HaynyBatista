using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Models;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using HaynyBatista.UtilClasses;
using HaynyBatista.Models.ViewModels;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HaynyBatista.Controllers
{
    public class ConsultaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Consulta
        public ActionResult Index()
        {
            IndiceConsulta ic = new IndiceConsulta()
            {
                Citas = db.Citas.ToList(),
                TiposCita = db.TiposCita.ToList(),
                FormasPago = db.FormasPago.ToList()
            };
            return View(ic);
        }
        [HttpGet]
        public ActionResult GetEventosMes()
        {
            var citas = db.Citas.Where(x => x.Fecha.Month == DateTime.Now.Month && x.Fecha.Year == DateTime.Now.Year).ToList();
            List<Object> eventos = new List<object>();
            foreach (Cita cita in citas)
            {
                eventos.Add(new
                {
                    title = "Cita",
                    start = cita.Fecha.Add(cita.HoraInicio).ToString("yyyy-MM-ddThh:mm:ss"),
                    end = cita.Fecha.Add(cita.HoraFin).ToString("yyyy-MM-ddThh:mm:ss"),
                    color = cita.EstadoCita.Color,
                    editable = false
                });
            }

            return Json(eventos.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ActualizarCita(ActualizarCitaViewModel CitaVM)
        {
            Retorno retorno = new Retorno();
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var user = db.Users.Find(User.Identity.GetUserId());
                    var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
                    var Cita = db.Citas.Find(CitaVM.IdCita);
                    if (Cita != null)
                    {
                        TimeSpan horaInicio;
                        TimeSpan horaFin;
                        if (TimeSpan.TryParse(CitaVM.HoraInicio, out horaInicio) && TimeSpan.TryParse(CitaVM.HoraFin, out horaFin))
                        {
                            if (Cita.Fecha != CitaVM.Fecha || Cita.HoraInicio != horaInicio || Cita.HoraFin != horaFin)
                            {
                                Cita.Fecha = CitaVM.Fecha;
                                Cita.HoraInicio = horaInicio;
                                Cita.HoraFin = horaFin;
                                if (MailSender.SendBasicEmail("HaynyBatista@haynybatista.com", "@Hayny.Batista", user.Email, "Solicitud de cita " + Cita.Fecha.ToString("dd/MM/yyyy"), FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaPospuesta.cshtml", Cita, false)))
                                {
                                    retorno = new Retorno() { Success = true, Message = String.Format("Se ha enviado un correo a {0} {1} notificandole sobre su cita pospuesta", Cita.Usuarios.First().Nombre, Cita.Usuarios.First().Apellido) };
                                }
                            }
                        }

                        if (Cita.IdEstadoCita != CitaVM.IdEstadoCita)
                        {
                            Cita.IdEstadoCita = CitaVM.IdEstadoCita;


                            switch (CitaVM.IdEstadoCita)
                            {
                                case 1:
                                    if (MailSender.SendBasicEmail("HaynyBatista@haynybatista.com", "@Hayny.Batista", user.Email, "Solicitud de cita " + Cita.Fecha.ToString("dd/MM/yyyy"), FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaSolicitada.cshtml", Cita, false)))
                                    {
                                        retorno = new Retorno() { Success = true, Message = String.Format("Se ha enviado un correo a {0} {1} notificandole sobre su cita pendiente", Cita.Usuarios.First().Nombre, Cita.Usuarios.First().Apellido) };
                                    }
                                    break;
                                case 2:
                                    if (MailSender.SendBasicEmail("HaynyBatista@haynybatista.com", "@Hayny.Batista", user.Email, "Solicitud de cita " + Cita.Fecha.ToString("dd/MM/yyyy"), FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaAprobada.cshtml", Cita, false)))
                                    {
                                        retorno = new Retorno() { Success = true, Message = String.Format("Se ha enviado un correo a {0} {1} notificandole sobre su cita aprobada", Cita.Usuarios.First().Nombre, Cita.Usuarios.First().Apellido) };
                                    }
                                    break;

                            }
                        }


                        db.Entry(Cita).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                    //var body = FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaSolicitada.cshtml", Cita, false);

                }
                catch (Exception e)
                {
                    retorno = new Retorno() { Success = false, Message = "No pudimos registrar su cita" };
                }

            }
            else
            {
                retorno = new Retorno() { Success = false, Message = "Debe iniciar sesión para reservar una cita." };
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
            //return Json(new { Caca = "asd" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GuardarCita(NuevaCitaViewModel NuevaCita)
        {

            Retorno retorno;
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var user = db.Users.Find(User.Identity.GetUserId());
                    var usuarioHayny = db.Usuarios.Find(user.Usuario.IdUsuario);
                    var TipoCita = db.TiposCita.Find(NuevaCita.IdTipoCita);
                    Cita Cita = new Cita()
                    {
                        Fecha = NuevaCita.FechaInicio.Date,
                        HoraInicio = NuevaCita.FechaInicio.TimeOfDay,
                        HoraFin = NuevaCita.FechaFin.TimeOfDay,
                        IdFormaPago = NuevaCita.FormaPagoID,
                        IdTipoCita = NuevaCita.IdTipoCita,
                        Mensaje = NuevaCita.Mensaje,
                        Costo = TipoCita.Costo,
                        IdEstadoCita = 1

                    };
                    Cita.Usuarios = new List<Usuario>();
                    Cita.Usuarios.Add(usuarioHayny);

                    db.Citas.Add(Cita);
                    db.SaveChanges();
                    //var body = FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaSolicitada.cshtml", Cita, false);
                    if (MailSender.SendEmailWithLogo("HaynyBatista@haynybatista.com", "@Hayny.Batista", user.Email, "Solicitud de cita " + Cita.Fecha.ToString("dd/MM/yyyy"), FakeController.RenderViewToString(this.ControllerContext, "~/Views/Correo/CitaSolicitada.cshtml", Cita, false)))
                    {
                        retorno = new Retorno() { Success = true, Message = "Nueva cita Registrada." };
                    }
                    else
                    {
                        retorno = new Retorno() { Success = true, Message = "Nueva cita Registrada. No se pudo generar el correo." };
                    }

                }
                catch (Exception e)
                {
                    retorno = new Retorno() { Success = false, Message = "No pudimos registrar su cita" };
                }

            }
            else
            {
                retorno = new Retorno() { Success = false, Message = "Debe iniciar sesión para reservar una cita." };
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}