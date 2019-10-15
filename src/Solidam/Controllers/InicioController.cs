using System.Web.Mvc;
using Services;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            //TestService.Instance.Post(null);
            return View();
        }
    }
}