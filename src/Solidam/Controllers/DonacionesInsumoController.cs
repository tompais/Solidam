﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class DonacionesInsumoController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public JsonResult Crear(List<DonacionesInsumos> donaciones)
        {
            foreach (var donacion in donaciones)
            {
                DonacionesInsumosService.Crear(donacion);
            }
            return Json("");
        }
    }
}