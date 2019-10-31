
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
        public static int IdPropuestaCompletada
        {
            get => int.Parse(CurrentSession["idPropuestaCompletada"].ToString());
            set => CurrentSession["idPropuestaCompletada"] = value;
        }

        public static bool MostrarPopUpCompletarPerfil
        {
            get => CurrentSession["mostrarPopUpCompletarPerfil"] as bool? ?? false;
            set => CurrentSession["mostrarPopUpCompletarPerfil"] = value;
        }

        public static bool MostrePopUpCompletarPerfil
        {
            get => CurrentSession["mostrePopUpCompletarPerfil"] as bool? ?? false;
            set => CurrentSession["mostrePopUpCompletarPerfil"] = value;
        }

    }
}
