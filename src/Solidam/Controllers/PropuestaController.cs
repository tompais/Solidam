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

        [HttpPost]
        public ActionResult Valorar(string mg, string nmg, PropuestasValoraciones pv)
        {
            PropuestasValoraciones propuestasValoraciones = new PropuestasValoraciones
            {
                IdPropuesta = pv.IdPropuesta,
                IdUsuario = SessionHelper.Usuario.IdUsuario,
                Valoracion = nmg == null
            };
            PropuestasValoracionesService.Crear(propuestasValoraciones);
            PropuestaService.PutPorcentajeAceptacion(pv.IdPropuesta);
            return RedirectToAction("Detalle", "Propuesta",new {id = pv.IdPropuesta });
        }

        public ActionResult Detalle(int id)
        {
            if (SessionHelper.Usuario == null)
            {
                TempData["pendingRoute"] = Url.Action("Detalle", "Propuesta",new { id = id });
                return RedirectToAction("Iniciar", "Seguridad");
            }
            var propuesta = PropuestaService.GetById(id);
            return View(propuesta);
        }
    }
}