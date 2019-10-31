using Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Exceptions;

namespace Solidam.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var usuarioSesion = SessionHelper.Usuario;
            if (!SessionHelper.MostrePopUpCompletarPerfil && usuarioSesion != null &&
                string.IsNullOrEmpty(usuarioSesion.Nombre) && string.IsNullOrEmpty(usuarioSesion.Apellido) &&
                string.IsNullOrEmpty(usuarioSesion.Foto) && string.IsNullOrEmpty(usuarioSesion.UserName))
            {
                SessionHelper.MostrarPopUpCompletarPerfil = SessionHelper.MostrePopUpCompletarPerfil = true;
            }
            else
            {
                SessionHelper.MostrarPopUpCompletarPerfil = false;
            }

            base.Initialize(requestContext);

            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            if (SessionHelper.Usuario == null && !controllerName.Equals(Constant.InicioControllerName.ToLower()) && !controllerName.Equals(Constant.SeguridadControllerName.ToLower()))
                throw new AccesoNoAutorizadoException(filterContext.HttpContext.Request.Url?.AbsoluteUri);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.Exception is AccesoNoAutorizadoException)
                filterContext.Result = RedirectToAction("Iniciar", "Seguridad");
        }
    }
}