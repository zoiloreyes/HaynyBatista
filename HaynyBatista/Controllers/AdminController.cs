﻿using HaynyBatista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace HaynyBatista.Controllers
{
    
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Usuario()
        {
            var Usuarios = (from user in db.Users
                            select new
                            {
                                UserId = user.Id,
                                UserName = user.UserName,
                                Email = user.Email,

                                RoleNames = (from userRole in user.Roles
                                             join role in db.Roles on userRole.RoleId
                                             equals role.Id
                                             select role.Name).ToList(),
                                Citas = user.Usuario.Citas,
                                Nombre = user.Usuario.Nombre,
                                Apellido = user.Usuario.Apellido
                            }).ToList().Select(p => new UsersInRoleViewModel()
                            {
                                UserId = p.UserId,
                                UserName = p.UserName,
                                Email = p.Email,
                                Nombre = p.Nombre,
                                Apellido = p.Apellido,
                                Citas = p.Citas,
                                Role = string.Join(",", p.RoleNames)
                            }

                                );

            return View(Usuarios);
        }

        public ActionResult Cita()
        {
            var vistas = db.Citas.Include(c => c.Usuarios).OrderBy(c => c.Fecha).ThenBy(c => c.HoraInicio).ToList();
            return View(vistas);
        }

        public ActionResult Tienda()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
    }
}