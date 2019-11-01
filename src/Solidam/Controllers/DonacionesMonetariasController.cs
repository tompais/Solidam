using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class DonacionesMonetariasController : BaseController
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Crear(DonarViewModel donacion)
        {
            ModelState.Remove("Propuesta.Nombre");
            ModelState.Remove("Propuesta.Descripcion");
            ModelState.Remove("Propuesta.FechaFin");
            ModelState.Remove("Propuesta.TelefonoContacto");
            ModelState.Remove("Propuesta.Foto");
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
                return View("~/Views/Propuesta/Donar.cshtml",dvm);
            }
            donacion.DonacionMonetaria.ArchivoTransferencia = FileHelper.GuardarArchivo(donacion.DonacionMonetaria.File);
            DonacionesMonetariasService.Crear(donacion.DonacionMonetaria);
            if (PropuestasDonacionesMonetariasService.EsCompletada(donacion.DonacionMonetaria))
            {
                SessionHelper.IdPropuestaCompletada = donacion.Propuesta.IdPropuesta;
                return RedirectToAction("Completada", "Propuesta");
            }

            return RedirectToAction("Donar","Propuesta",new {id = donacion.DonacionMonetaria.PropuestasDonacionesMonetarias.IdPropuesta});
        }
    }
}