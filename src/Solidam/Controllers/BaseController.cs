using Helpers;
using System.Web.Mvc;
using Exceptions;

namespace Solidam.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            if (SessionHelper.Usuario == null && !controllerName.Equals(Constant.InicioControllerName.ToLower()) && !controllerName.Equals(Constant.SeguridadControllerName.ToLower()))
                throw new AccesoNoAutorizadoException(filterContext.HttpContext.Request.Url?.AbsolutePath);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.Exception is AccesoNoAutorizadoException)
                filterContext.Result = RedirectToAction("Iniciar", "Seguridad");
        }
    }
}