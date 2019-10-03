using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Core.Logging;
using Helpers;
using ILogger = NLog.ILogger;

namespace Solidam
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog Logger { get; } = LoggingHelper.GetLogger<MvcApplication>();
        public override void Init()
        {
            base.Init();
            Error += MvcApplication_Error;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureFirstChanceExceptionsHandler();
        }

        private static void ConfigureFirstChanceExceptionsHandler()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                Logger.Error(eventArgs.Exception);
            };
        }

        private void MvcApplication_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception != null)
                Logger.Error(exception);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception != null)
                Logger.Error(exception);
        }
    }
}