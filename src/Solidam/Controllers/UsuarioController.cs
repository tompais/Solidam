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
        [Route("perfil")]
        public ActionResult MiPerfil()
        {
            return View(new UsuarioPerfil(SessionHelper.Usuario));
        }

        [HttpPost]
        public ActionResult GuardarPerfil(UsuarioPerfil perfil)
        {
            if (!ModelState.IsValid) throw new PerfilInvalidoException();

            SessionHelper.Usuario = UsuarioService.Instance.Put(new Usuarios
            {
                IdUsuario = SessionHelper.Usuario.IdUsuario,
                Nombre = perfil.Nombre,
                Apellido = perfil.Apellido,
                FechaNacimiento = perfil.FechaNacimiento,
                Foto = FileHelper.GuardarArchivo(perfil.ProfilePicFile),
                UserName = UsuarioService.Instance.GenerarUserName(perfil.Nombre, perfil.Apellido)
            });

            return RedirectToAction("MiPerfil");
        }
    }
}