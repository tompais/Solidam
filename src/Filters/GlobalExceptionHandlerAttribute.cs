using Exceptions;
using Helpers;
using System.Net;
using System.Web.Mvc;

namespace Filters
{
    public class GlobalExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            if (filterContext.HttpContext.Request.IsAjaxRequest() && exception != null)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = exception is SolidamException e ? e : exception
                };
                filterContext.ExceptionHandled = true;
            }
            else
                base.OnException(filterContext);

            LoggingHelper.LogException(exception);
        }
    }
}