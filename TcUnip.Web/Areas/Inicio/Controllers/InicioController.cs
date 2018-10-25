using System.Web.Mvc;
using TCC_Unip.Controllers;

namespace TCC_Unip.Areas.Inicio.Controllers
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