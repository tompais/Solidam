using System.Net;
using System.Web.Mvc;
using Exceptions;
using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Filters
{
    public class GlobalExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception == null) return;
            HandleCustomException(filterContext);
            base.OnException(filterContext);
            LoggingHelper.LogException(filterContext.Exception);
        }

        private static void HandleCustomException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var response = filterContext.HttpContext.Response;
            response.ContentType = Constant.ApplicationJsonUtf8ContentType;
            if (filterContext.Exception is SolidamException) response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var exceptionResponse = new ExceptionResponse(exception);
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = exceptionResponse
                };
            }
            var jsonResponse = JsonConvert.SerializeObject(exceptionResponse,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            response.Write(jsonResponse);
        }
    }
}