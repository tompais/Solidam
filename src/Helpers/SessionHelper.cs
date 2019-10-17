
using System.Collections.Generic;
using System.Web;
using Models;

namespace Helpers
{
    public class SessionHelper
    {
        public static Usuario Usuario
        {
            get => HttpContext.Current.Session["usuario"] as Usuario;

            set => HttpContext.Current.Session["usuario"] = value;
        }

    }
}
