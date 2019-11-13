using Filters;
using System.Web.Mvc;

namespace Solidam
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new GlobalExceptionHandlerAttribute());
        }
    }
}