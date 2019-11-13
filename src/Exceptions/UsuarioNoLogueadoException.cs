using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Exceptions
{
    public class UsuarioNoLogueadoException : SolidamException
    {
        public string Url { get; set; }
        public override string Message => $"No se ha podido acceder a la Url '{(string.IsNullOrEmpty(Url) ? "(Desconocida)" : Url)}' porque no se encuentra logueado.";
        public override ErrorCode ErrorCode => ErrorCode.UsuarioNoLogueado;

        public UsuarioNoLogueadoException(string url)
        {
            Url = url;
        }
    }
}
