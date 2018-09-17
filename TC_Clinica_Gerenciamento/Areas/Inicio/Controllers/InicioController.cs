using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_Clinica_Gerenciamento.Models.Local;

namespace TC_Clinica_Gerenciamento.Areas.Inicio.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio/Inicio
        public ActionResult Index()
        {
            if ((User)Session["user"] == null)
                return RedirectToAction("Login", "Login", new { area = "" });

            return View();
        }
    }
}