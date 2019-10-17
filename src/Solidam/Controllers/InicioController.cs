using System.Web.Mvc;
using Services;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            //Dejo comentado para probar la conexion a la bdd
            //TestService.Instance.Post(null);
            return View();
        }
    }
}