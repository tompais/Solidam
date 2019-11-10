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
                PropuestasTop5 = PropuestaService.GetPropuestasMasValoradas(),
                PropuestasActivas = PropuestaService.TodasLasPropuestasActivas()
            };
            return View(inicioViewModel);
        }

        public ActionResult IniciarSesion()
        {
            return View("Inicio");
        }

        [Route("acerca-de")]
        public ActionResult AcercaDe()
        {
            return View("");
        }
    }
}