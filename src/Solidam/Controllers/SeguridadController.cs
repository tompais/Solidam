using System.Linq;
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
        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Loguear(UsuariosLogin model)
        {
            var inicioViewModel = new InicioViewModel();

            if (!ModelState.IsValid)
            {
                return View("Iniciar", model);
            }

            var usuarioLoguear = SeguridadService.Instance.Get(new Usuarios { Email = model.Email, Password = model.Password }).FirstOrDefault();

            SessionHelper.Usuario = usuarioLoguear;

            if (TempData["pendingRoute"] == null) return RedirectToAction("Inicio", "Inicio");
            var rutaPendiente = TempData["pendingRoute"].ToString();
            TempData["pendingRoute"] = null;
            return Redirect(rutaPendiente);

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