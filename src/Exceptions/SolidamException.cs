using Enums;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Exceptions
{
    public class SolidamException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public SolidamException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public SolidamException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public SolidamException(string message, Exception innerException, ErrorCode errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        protected SolidamException(SerializationInfo info, StreamingContext context, ErrorCode errorCode) : base(info, context)
        {
            ErrorCode = errorCode;
        }

        public string GetCustomErrorMessage()
        {
            var stackFrame = new StackTrace(this, true).GetFrame(0);
            var nombreArchivo = stackFrame.GetFileName();
            var metodo = stackFrame.GetMethod();
            var nombreClase = metodo.DeclaringType;
            var nombreMetodo = metodo.Name;
            var numeroLinea = stackFrame.GetFileLineNumber();
            return $"Error Nro {(int)ErrorCode}: '{Message}' en el archivo '{nombreArchivo}' en la clase '{nombreClase}' en el método '{nombreMetodo}' en la línea {numeroLinea}\n\nStackTrace: {StackTrace}";
        }
    }
}