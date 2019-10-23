using System.Web.Mvc;
using Services;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            //var a = TestService.Get();
            return View();
        }

        public ActionResult IniciarSesion()
        {
            return View("Inicio");
        }
    }
}