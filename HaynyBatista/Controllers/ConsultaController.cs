using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Models;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using HaynyBatista.UtilClasses;

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
                TiposCita = db.TiposCita.ToList()
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
                eventos.Add(new { title = "Cita",
                    start = cita.Fecha.Add(cita.HoraInicio).ToString("yyyy-MM-ddThh:mm:ss"),
                    end = cita.Fecha.Add(cita.HoraFin).ToString("yyyy-MM-ddThh:mm:ss"),
                    color = cita.EstadoCita.Color,
                    editable = false
                });
            }

            return Json(eventos.ToArray(), JsonRequestBehavior.AllowGet);
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
                        TipoCita = TipoCita,
                        Mensaje = NuevaCita.Mensaje,
                        Costo = TipoCita.Costo,
                        IdEstadoCita = 1

                    };
                    Cita.Usuarios = new List<Usuario>();
                    Cita.Usuarios.Add(usuarioHayny);

                    db.Citas.Add(Cita);
                    db.SaveChanges();
                    if (MailSender.SendBasicEmail("zoiloismaelreyes1@gmail.com", "scottie5", "zoiloismaelreyes1@gmail.com", "Probando esto", "jojojo")){
                        retorno = new Retorno() { Success = true, Message = "Nueva cita Registrada." };
                    }
                    else
                    {
                        retorno = new Retorno() { Success = true, Message = "Nueva cita Registrada. No se pudo generar el correo." };
                    }
                    
                }
                catch(Exception e)
                {
                    retorno = new Retorno() { Success = false, Message = "No pudimos registrar su cita" };
                }
                
            }
            else
            {
                retorno = new Retorno() { Success = false, Message = "Debe iniciar sesión para reservar una cita." };
            }
            
            return Json(retorno,JsonRequestBehavior.AllowGet);
        }
    }
}