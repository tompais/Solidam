using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helpers;
using Models;

namespace Solidam.Controllers
{
    public class UsuarioController : BaseController
    {
        // GET: Perfil
        [Route("miPerfil")]
        public ActionResult MiPerfil()
        {
            return View(new UsuarioPerfil(SessionHelper.Usuario));
        }

        [HttpPost]
        public ActionResult GuardarPerfil()
        {
            return null;
        }
    }
}