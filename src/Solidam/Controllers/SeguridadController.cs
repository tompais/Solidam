using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Helpers;
using Models;
using Services;
using Solidam.ViewModel;

namespace Solidam.Controllers
{
    public class SeguridadController : Controller
    {
        [Route("login")]
        public ActionResult Iniciar(string pr)
        {
            ViewBag.PendingRoute = pr;
            return View();
        }

        [HttpPost]
        public ActionResult Loguear(UsuariosLogin model, string pr)
        {
            var inicioViewModel = new InicioViewModel();

            if (!ModelState.IsValid)
            {
                return View("Iniciar", model);
            }

            var usuarioLoguear = SeguridadService.Instance.Get(new Usuarios { Email = model.Email, Password = model.Password }).FirstOrDefault();

            SessionHelper.Usuario = usuarioLoguear;

            if (pr == null) return RedirectToAction("Inicio", "Inicio");
            return Redirect(Encoding.ASCII.GetString(Convert.FromBase64String(pr)));

        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(UsuariosRegister model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuarioEvaluado = SeguridadService.Instance.Post(new Usuarios { Email = model.Email, Password = model.Password, FechaNacimiento = model.FechaNacimiento });

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