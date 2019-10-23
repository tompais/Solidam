using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Solidam.ViewModel;
using Utils;

namespace Solidam.Controllers
{
    public class SeguridadController : Controller
    {
        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion(Usuarios usuario)
        {
            var inicioViewModel = new InicioViewModel();
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            var usuarioIniciar = SeguridadService.Instance.Get(usuario).FirstOrDefault();

            if (usuarioIniciar == null)
            {
                usuarioIniciar = new Usuarios { Error = "Email y/o Contraseña inválidos" };
                //inicioViewModel.Usuario = usuarioIniciar;
                return View("Iniciar", usuarioIniciar);
            }

            if (usuarioIniciar.Activo.ToString().Equals("False"))
            {
                usuarioIniciar = new Usuarios { Error = "Su usuario está inactivo. Actívelo desde el email recibido" };
                //inicioViewModel.Usuario = usuarioIniciar;
                return View("Iniciar", usuarioIniciar);
            }

            SessionHelper.Usuario = usuarioIniciar;

            return RedirectToAction("Inicio", "Inicio");
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(Usuarios usuario)
        {
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