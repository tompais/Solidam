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
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Propuestas p, System.Web.HttpPostedFileBase foto)
        {
            string path = Server.MapPath("~/Images/Views/Propuesta/");

            p.Foto = path + System.IO.Path.GetFileName(foto.FileName);

            PropuestaService.AgregarPropuesta(p);

            foto.SaveAs(path + System.IO.Path.GetFileName(foto.FileName));

            return RedirectToAction("Inicio", "Inicio");
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
            return RedirectToAction("Inicio", "Inicio");
        }
    }
}