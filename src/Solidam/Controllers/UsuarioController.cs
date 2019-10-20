using Helpers;
using Models;
using Services;
using System.Linq;
using System.Web.Mvc;
using Utils;

namespace Solidam.Controllers
{
    public class UsuarioController : BaseController
    {
        public ActionResult Iniciar()
        {
            return View();
        }

        [HttpGet]
        public ActionResult IniciarSesion(Usuario usuario)
        {
            usuario.Password = Sha1.GetSHA1(usuario.Password);

            var usuarioIniciar = UsuarioService.Instance.Get(usuario).FirstOrDefault();

            if (usuarioIniciar == null)
            {
                usuarioIniciar = new Usuario { Error = "Email y/o Contraseña inválidos" };
                return View("Iniciar", usuarioIniciar);
            }

            if (usuarioIniciar.Activo.ToString().Equals("False"))
            {
                usuarioIniciar = new Usuario { Error = "Su usuario está inactivo. Actívelo desde el email recibido" };
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
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            var usuarioEvaluado = UsuarioService.Instance.Post(usuario);

            if (usuarioEvaluado == null) return View("Registrar", usuarioEvaluado);
            UsuarioService.Instance.EnviarCorreo(usuarioEvaluado.Token, usuarioEvaluado.Email);
            return View("RegistroExitoso");
        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Activar(string token)
        {
            UsuarioService.Instance.Put(new Usuario { Token = token, Activo = true });

            return View("ActivacionExitosa");
        }

        public ActionResult CerrarSession()
        {
            SessionHelper.Usuario = null;
            return RedirectToAction("Inicio", "Inicio");
        }
    }
}