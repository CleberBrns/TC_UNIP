using System.Web.Mvc;

namespace TCC_Unip.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {            
            if (!GetUsuarioSession().Item2)
                return RedirectToAction("Login", "Login");

            return View();
        }
    }
}