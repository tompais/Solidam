﻿using System;
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
            donacion.DonacionMonetaria.ArchivoTransferencia = donacion.DonacionMonetaria.File.FileName;
            FileHelper.GuardarArchivo(donacion.DonacionMonetaria.File);
            DonacionesMonetariasService.Crear(donacion.DonacionMonetaria);
            return RedirectToAction("Donar","Propuesta",new {id = donacion.DonacionMonetaria.PropuestasDonacionesMonetarias.IdPropuesta});
        }
    }
}