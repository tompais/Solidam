using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using Helpers;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class DonacionesInsumoController : BaseController
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Donar(List<DonacionesInsumos> donaciones)
        {
            var propuestaId =
                PropuestasDonacionesInsumosService.GetPropuestaId(donaciones[0].IdPropuestaDonacionInsumo);
            if (!ModelState.IsValid)
            {
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

            if (PropuestasDonacionesInsumosService.EsCompletada(donaciones))
            {
                SessionHelper.IdPropuestaCompletada = propuestaId;
                return RedirectToAction("Completada", "Propuesta");
            }
            return RedirectToAction("Detalle", "Propuesta", new { id = propuestaId });
        }
        [HttpGet]
        public ActionResult Donar(int id)
        {
            var propuesta = PropuestaService.GetById(id);
            DonarViewModel dvm = new DonarViewModel
            {
                Propuesta = propuesta,
                DonacionesMonetarias = DonacionesMonetariasService.GetById(id),
                DonacionesHorasTrabajo = DonacionesHorasTrabajoService.GetById(id),
                DonacionesInsumos = DonacionesInsumosService.GetById(id)
            };
            return View("~/Views/Propuesta/Donar.cshtml", dvm);
        }
        [HttpPost]
        public ActionResult SolicitarCantidades(List<DonacionesInsumoDto> donaciones)
        {
            ViewBag.donaciones = donaciones.Where(d => d.Seleccionado != null && d.Seleccionado.Equals("on")).ToList();
            return View();
        }
    }
}