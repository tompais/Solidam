using System.Web.Mvc;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            //var a = TestService.Get();
            InicioViewModel inicioViewModel = new InicioViewModel();
            inicioViewModel.Propuestas = PropuestaService.GetPropuestasMasValoradas();
            return View(inicioViewModel);
        }

        public ActionResult IniciarSesion()
        {
            return View("Inicio");
        }
    }
}