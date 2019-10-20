using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Utils;

namespace Solidam.Controllers
{
    public class PropuestaController : BaseController
    {
        public ActionResult CrearPropuesta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Propuesta p)
        {
            return View();
        }
    }
}