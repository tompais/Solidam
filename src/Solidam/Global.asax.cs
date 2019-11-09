using Helpers;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Solidam
{
    public class MvcApplication : HttpApplication
    {
        public override void Init()
        {
            base.Init();
            Error += MvcApplication_Error;
        }

        public override void Dispose()
        {
            base.Dispose();
            LoggingHelper.FlushLogger();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoggingHelper.Configure(typeof(MvcApplication));
            ConfigureUnhandledExceptionsHandler();
        }

        private static void ConfigureUnhandledExceptionsHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_CurrentDomain_UnhandledException;
        }

        private static void AppDomain_CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // use logger here to log the events exception object
            // before the application quits
            LoggingHelper.LogException((Exception)e.ExceptionObject);
        }

        private void MvcApplication_Error(object sender, EventArgs e)
        {
            LoggingHelper.LogException(Server.GetLastError());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            LoggingHelper.LogException(exception);
            Response.Clear();
            var error = exception is HttpException httpException ? httpException.GetHttpCode() : 0;
            Server.ClearError();
            Response.Redirect($"~/home/error/?codigo={error}");
        }
    }
}