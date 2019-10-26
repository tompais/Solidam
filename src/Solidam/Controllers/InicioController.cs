using System.Web.Mvc;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        [Route("")]
        public ActionResult Inicio()
        {
            //var a = TestService.Get();
            var inicioViewModel = new InicioViewModel
            {
                Propuestas = PropuestaService.GetPropuestasMasValoradas()
            };
            return View(inicioViewModel);
        }

        public ActionResult IniciarSesion()
        {
            return View("Inicio");
        }
    }
}