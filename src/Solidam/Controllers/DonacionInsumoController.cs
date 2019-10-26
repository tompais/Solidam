using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;

namespace Solidam.Controllers
{
    public class DonacionInsumoController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public JsonResult Crear(List<DonacionesInsumos> donaciones)
        {
            foreach (var donacion in donaciones)
            {
                DonacionesInsumosService.Crear(donacion);
                //PropuestasDonacionesInsumosService.Update(donacion);
            }
            return Json("");
        }
    }
}