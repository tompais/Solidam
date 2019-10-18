using Helpers;
using Models;
using Services;
using System.Web.Mvc;

namespace Solidam.Controllers
{
    public class UsuarioController : BaseController
    {

        UsuarioService UsuarioService = new UsuarioService();

        public ActionResult Iniciar()
        {
            return View();
        }

        public ActionResult IniciarSesion(Usuario usuario)
        {

            Usuario usuarioIniciar = UsuarioService.BuscarUsuario(usuario);

            if (usuarioIniciar == null)
            {
                usuarioIniciar = new Usuario() { ErorrLogueo = "Email y/o Contraseña inválidos" };
                return View("Iniciar", usuarioIniciar);
            }
            if (usuarioIniciar.Activo.ToString().Equals("False"))
            {
                usuarioIniciar = new Usuario() { ErorrLogueo = "Su usuario está inactivo. Actívelo desde el email recibido" };
                return View("Iniciar", usuarioIniciar);
            }
            else
            {
                return RedirectToAction("Inicio", "Inicio");
            }
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult RegistrarUsuario(Usuario usuario)
        {

            UsuarioService.RegistrarUsuario(usuario);

            return View("RegistroExitoso");
        }

        public ActionResult RegistroExitoso()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Activar(string token)
        {
            UsuarioService.ActivarUsuario(token);

            return View("ActivacionExitosa");
        }

        public ActionResult CerrarSession()
        {
            SessionHelper.Usuario = null;
            return RedirectToAction("Inicio", "Inicio");
        }

    }
}