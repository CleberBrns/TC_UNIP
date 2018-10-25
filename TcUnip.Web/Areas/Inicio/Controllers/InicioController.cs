using System.Web.Mvc;
using TcUnip.Web.Controllers;

namespace TcUnip.Web.Areas.Inicio.Controllers
{
    public class InicioController : BaseController
    {
        public ActionResult Index()
        {
            var userInfo = GetUsuarioSession();

            if (!userInfo.Item2)
                return RedirectToAction("Login", "Login", new { area = "" });

            ViewBag.Usuario = userInfo.Item1;

            return View();
        }
    }
}