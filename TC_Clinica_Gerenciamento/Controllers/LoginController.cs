using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TC_Clinica_Gerenciamento.Models.Local;

namespace TC_Clinica_Gerenciamento.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if ((User)Session["user"] != null)
                return RedirectToAction("Index", "Inicio");            

            return View();
        }

        [HttpPost]
        public ActionResult Login(string nome, string senha)
        {
            if (nome.Equals("admin") && senha.Equals("56784321"))
            {
                Session["user"] = new User() { Login = nome, Nome = "Administrador" };
                return RedirectToAction("Index", "Inicio");
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
                Session["user"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}