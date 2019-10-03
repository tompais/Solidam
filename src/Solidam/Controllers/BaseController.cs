using System.Web.Mvc;
using Helpers;

namespace Solidam.Controllers
{
    public class BaseController : Controller
    {
        private static log4net.ILog Logger { get; } = LoggingHelper.GetLogger<BaseController>();

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            filterContext.ExceptionHandled = true;

            Logger.Error(filterContext.Exception);
        }
    }
}