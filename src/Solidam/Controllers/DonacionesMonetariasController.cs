using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
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
            donacion.ArchivoTransferencia = donacion.File.FileName;
            FileHelper.GuardarArchivo(donacion.File);
            DonacionesMonetariasService.Crear(donacion);
            return RedirectToAction("Donar","Propuesta",new {id = donacion.PropuestasDonacionesMonetarias.IdPropuesta});
        }
    }
}