using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace Solidam.Controllers
{
    public class DonacionInsumoController : Controller
    {
        // GET: DonacionInsumo
        public JsonResult Crear(List<DonacionesInsumos> donaciones)
        {
            return Json("");
        }
    }
}