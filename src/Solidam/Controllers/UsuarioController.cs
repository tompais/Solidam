using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exceptions;
using Helpers;
using Models;
using Services;

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
        public ActionResult GuardarPerfil(UsuarioPerfil perfil)
        {
            if (!ModelState.IsValid) throw new PerfilInvalidoException();

            SessionHelper.Usuario.Nombre = perfil.Nombre;
            SessionHelper.Usuario.Apellido = perfil.Apellido;
            SessionHelper.Usuario.FechaNacimiento = perfil.FechaNacimiento;

            SessionHelper.Usuario = UsuarioService.Instance.Put(SessionHelper.Usuario);

            return RedirectToAction("MiPerfil");
        }
    }
}