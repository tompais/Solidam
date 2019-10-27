using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class DonacionesInsumoController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Crear(List<DonacionesInsumos> donaciones)
        {
            var propuestaId =
                PropuestasDonacionesInsumosService.GetPropuestaId(donaciones[0].IdPropuestaDonacionInsumo);
            if (!ModelState.IsValid)
            {
                //var propuesta = PropuestaService.GetById(propuestaId);
                //DonarViewModel dvm = new DonarViewModel
                //{
                //    Propuesta = propuesta,
                //    DonacionesMonetarias = DonacionesMonetariasService.GetById(propuestaId),
                //    DonacionesHorasTrabajo = DonacionesHorasTrabajoService.GetById(propuestaId),
                //    DonacionesInsumos = DonacionesInsumosService.GetById(propuestaId)
                //};
                ViewBag.donaciones = donaciones.Select(d => new DonacionesInsumoDto
                {
                    IdPropuesta = propuestaId,
                    IdPropuestaDonacionInsumo = d.IdPropuestaDonacionInsumo,
                    Nombre = PropuestasDonacionesInsumosService.GetById(d.IdPropuestaDonacionInsumo).Nombre,
                });
                return View("~/Views/DonacionesInsumo/SolicitarCantidades.cshtml");
            }
            foreach (var donacion in donaciones)
            {
                DonacionesInsumosService.Crear(donacion);
            }
            return RedirectToAction("Donar", "Propuesta", new { id = propuestaId });
        }

        [HttpPost]
        public ActionResult SolicitarCantidades(List<DonacionesInsumoDto> donaciones)
        {
            ViewBag.donaciones = donaciones.Where(d => d.Seleccionado != null && d.Seleccionado.Equals("on")).ToList();
            return View();
        }
    }
}