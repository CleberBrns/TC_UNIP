using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_Clinica_Gerenciamento.Models.Local;

namespace TC_Clinica_Gerenciamento.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if ((User)Session["user"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }
    }
}