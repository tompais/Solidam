using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Services;

namespace Solidam.Controllers
{
    public class DonacionesMonetariasController : Controller
    {
        // GET: DonacionInsumo
        [HttpPost]
        public ActionResult Crear(DonacionesMonetarias donacion)
        {

            DonacionesMonetariasService.Crear(donacion);
            //PropuestasDonacionesMonetariasService.Update(donacion);
            return RedirectToAction("Donar","Propuesta",new {id = donacion.PropuestasDonacionesMonetarias.IdPropuesta});
        }
    }
}