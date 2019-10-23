using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Utils;
using Enums;

namespace Solidam.Controllers
{
    public class PropuestaController : BaseController
    {
        public ActionResult CrearPropuesta()
        {
            ViewBag.total = PropuestaService.TotalPropuestasActivas();
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Propuestas p)
        {
            PropuestaService.AgregarPropuesta(p);
            return View();
        }
    }
}