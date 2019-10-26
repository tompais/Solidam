using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;

namespace Solidam.Controllers
{
    public class DonacionesHorasTrabajoController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Crear(DonacionesHorasTrabajo donacion)
        {
            DonacionesHorasTrabajoService.Crear(donacion);
            PropuestasDonacionesHorasTrabajoService.Update(donacion);
            return RedirectToAction("Donar","Propuesta",new {id = donacion.PropuestasDonacionesHorasTrabajo.IdPropuesta});
        }
    }
}