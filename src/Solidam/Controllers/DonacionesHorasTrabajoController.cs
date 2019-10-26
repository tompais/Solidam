using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class DonacionesHorasTrabajoController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Crear(DonarViewModel donacion)
        {
            if (!ModelState.IsValid)
            {
                var propuesta = PropuestaService.GetById(donacion.Propuesta.IdPropuesta);
                DonarViewModel dvm = new DonarViewModel
                {
                    Propuesta = propuesta,
                    DonacionesMonetarias = DonacionesMonetariasService.GetById(donacion.Propuesta.IdPropuesta),
                    DonacionesHorasTrabajo = DonacionesHorasTrabajoService.GetById(donacion.Propuesta.IdPropuesta),
                    DonacionesInsumos = DonacionesInsumosService.GetById(donacion.Propuesta.IdPropuesta)
                };
                return View("~/Views/Propuesta/Donar.cshtml", dvm);
            }
            DonacionesHorasTrabajoService.Crear(donacion.DonacionHorasTrabajo);
            return RedirectToAction("Donar","Propuesta",new {id = donacion.DonacionHorasTrabajo.PropuestasDonacionesHorasTrabajo.IdPropuesta});
        }
    }
}