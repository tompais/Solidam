using System;
using System.ComponentModel.DataAnnotations;
using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Solidam.ViewModel;
using Utils;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter.Xml;

namespace Solidam.Controllers
{
    public class SeguridadController : Controller
    {
        [Route("/login")]
        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar(Usuarios usuario)
        {
            var inicioViewModel = new InicioViewModel();

            usuario.PasswordLogin = Sha1.GetSHA1(usuario.PasswordLogin);
            
            ModelState.Remove("Email");
            ModelState.Remove("FechaDeNacimiento");
            ModelState.Remove("Repassword");
            ModelState.Remove("Password");

            if (!ModelState.IsValid)
            {
                return View("Iniciar", usuario);
            }

            SessionHelper.Usuario = SeguridadService.Instance.Get(usuario).FirstOrDefault();

            if (TempData["pendingRoute"] != null)
            {
                var rutaPendiente = TempData["pendingRoute"].ToString();
                TempData["pendingRoute"] = null;
                return Redirect(rutaPendiente);
            }

            return RedirectToAction("Inicio", "Inicio");

        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuarios usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            var usuarioEvaluado = SeguridadService.Instance.Post(usuario);

            if (usuarioEvaluado == null) return View("Registrar", usuarioEvaluado);

            SeguridadService.Instance.EnviarCorreo(usuarioEvaluado.Token, usuarioEvaluado.Email);

            return View("RegistroExitoso");

        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Activar(string token)
        {
            SeguridadService.Instance.Put(new Usuarios { Token = token, Activo = true });

            return View("ActivacionExitosa");
        }

        public ActionResult CerrarSession()
        {
            SessionHelper.Usuario = null;
            return RedirectToAction("Inicio", "Inicio");
        }
    }
}