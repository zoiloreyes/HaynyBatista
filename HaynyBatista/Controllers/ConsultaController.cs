﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Models;
namespace HaynyBatista.Controllers
{
    public class ConsultaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Consulta
        public ActionResult Index()
        {
            return View(db.Citas.ToList());
        }
    }
}