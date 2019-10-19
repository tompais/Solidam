using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Enums;

namespace Exceptions
{
    public class UsuarioException : SolidamException
    {
        public UsuarioException(ErrorCode errorCode) : base(errorCode)
        {
        }

        public UsuarioException(string message, ErrorCode errorCode) : base(message, errorCode)
        {
        }

        public UsuarioException(string message, Exception innerException, ErrorCode errorCode) : base(message, innerException, errorCode)
        {
        }

        protected UsuarioException(SerializationInfo info, StreamingContext context, ErrorCode errorCode) : base(info, context, errorCode)
        {
        }
    }
}
