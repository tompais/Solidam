using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Exceptions
{
    public class AccesoNoAutorizadoException : SolidamException
    {
        private string Url { get; set; }
        public override string Message => $"No se ha podido acceder a la Url '{(string.IsNullOrEmpty(Url) ? "(Desconocida)" : Url)}'. Su acceso no está autorizado.";
        public override ErrorCode ErrorCode => ErrorCode.AccesoNoAutorizado;

        public AccesoNoAutorizadoException(string url)
        {
            Url = url;
        }
    }
}
