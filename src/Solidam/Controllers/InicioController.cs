﻿using System.Web.Mvc;

namespace Solidam.Controllers
{
    public class InicioController : BaseController
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult IniciarSesion()
        {
            return View("Inicio");
        }
    }
}