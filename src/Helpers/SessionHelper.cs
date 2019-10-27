
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Models;

namespace Helpers
{
    public class SessionHelper
    {
        private static HttpSessionState CurrentSession => HttpContext.Current.Session;
        public static Usuarios Usuario
        {
            get => CurrentSession["usuario"] as Usuarios;

            set => CurrentSession["usuario"] = value;
        }

    }
}
