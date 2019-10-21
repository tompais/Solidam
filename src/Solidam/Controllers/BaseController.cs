using Helpers;
using System.Web.Mvc;

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
                filterContext.Result = RedirectToAction("Inicio", "Inicio");
        }
    }
}