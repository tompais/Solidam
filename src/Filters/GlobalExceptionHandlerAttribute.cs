using Exceptions;
using Helpers;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Filters
{
    public class GlobalExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            HandleCustomException(filterContext);
            base.OnException(filterContext);
            LoggingHelper.LogException(filterContext.Exception);
        }

        private static void HandleCustomException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Headers.Clear();
            var response = filterContext.HttpContext.Response;
            response.ContentType = "application/json";
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new ExceptionResponse(exception)
            };
            var jsonResponse = JsonConvert.SerializeObject(new ExceptionResponse(exception),
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            response.Write(jsonResponse);
        }
    }
}