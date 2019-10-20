using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solidam.Controllers
{
    public class PerfilController : Controller
    {
        [Route("/perfil")]
        public ActionResult MiPerfil()
        {
            return View();
        }
    }
}