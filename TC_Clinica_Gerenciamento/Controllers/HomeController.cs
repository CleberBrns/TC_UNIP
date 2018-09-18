using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TCC_Unip.Models.Local;

namespace TCC_Unip.Controllers
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