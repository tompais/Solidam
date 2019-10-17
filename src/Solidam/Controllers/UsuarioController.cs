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
               usuarioIniciar = new Usuario() {ErorrLogueo = "Usuario/Contraseña incorrecto" };
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

        public ActionResult CerrarSession()
        {
            SessionHelper.Usuario = null;
            return RedirectToAction("Inicio", "Inicio");
        }

    }
}