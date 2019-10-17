using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solidam.Controllers
{
    public class UsuarioController : BaseController
    {
        // GET: Usuario
        public ActionResult Iniciar()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

    }
}