using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Exceptions
{
    public class UsuariosException : SolidamException
    {
        public UsuariosException(ErrorCode errorCode) : base(errorCode)
        {
        }

        public UsuariosException(string message, ErrorCode errorCode) : base(message, errorCode)
        {
        }

        public UsuariosException(string message, Exception innerException, ErrorCode errorCode) : base(message, innerException, errorCode)
        {
        }

        protected UsuariosException(SerializationInfo info, StreamingContext context, ErrorCode errorCode) : base(info, context, errorCode)
        {
        }
    }
}
