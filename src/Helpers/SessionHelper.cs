
using System.Collections.Generic;
using System.Web;
using Models;

namespace Helpers
{
    public class SessionHelper
    {
        public static Usuarios Usuario
        {
            get => HttpContext.Current.Session["usuario"] as Usuarios;

            set => HttpContext.Current.Session["usuario"] = value;
        }

    }
}
