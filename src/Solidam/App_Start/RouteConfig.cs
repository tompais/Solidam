using System.Web.Mvc;
using System.Web.Routing;

namespace Solidam
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Inicio", action = "Inicio", id = UrlParameter.Optional }
            );
        }
    }
}