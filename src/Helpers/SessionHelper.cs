
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Models;

namespace Helpers
{
    public static class SessionHelper
    {
        private static HttpSessionState CurrentSession => HttpContext.Current.Session;

        public static Usuario Usuario
        {
            get => CurrentSession["usuario"] as Usuario;

            set => CurrentSession["usuario"] = value;
        }

    }
}
