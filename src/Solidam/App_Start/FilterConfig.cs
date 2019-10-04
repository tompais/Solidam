﻿using System.Web.Mvc;
using Filters;

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