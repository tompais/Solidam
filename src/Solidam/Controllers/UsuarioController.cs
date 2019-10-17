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

            UsuarioService.BuscarUsuario(usuario);

            return RedirectToAction("Inicio", "Inicio");
        }

        public ActionResult Registrar()
        {
            return View();
        }

    }
}