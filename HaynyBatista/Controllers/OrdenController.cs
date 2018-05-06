using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HaynyBatista.Infrastructure.Attributes;
namespace HaynyBatista.Controllers
{
    [LoginRedirectAuthorize]
    public class OrdenController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}