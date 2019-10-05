using Filters;
using Helpers;
using System;
using System.Web;
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
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoggingHelper.Configure(typeof(MvcApplication));
            ConfigureFirstChanceExceptionsHandler();
            ConfigureUnhandledExceptionsHandler();
        }

        private static void ConfigureFirstChanceExceptionsHandler()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                LoggingHelper.LogException(eventArgs.Exception);
            };
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
            LoggingHelper.LogException(Server.GetLastError());
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionHandlerAttribute());
        }
    }
}