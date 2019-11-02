using System;
using System.Linq.Expressions;

namespace Exceptions
{
    public class ExceptionResponse
    {
        public ExceptionResponse(Exception exception)
        {
            if (exception is SolidamException solidamException)
            {
                InternalErrorCode = (int?)solidamException.ErrorCode;
                Message = solidamException.Message;
            }
            else
            {
                InternalErrorCode = null;
                Message = "Ha ocurrido un error interno no controlado en el sistema";
            }
        }

        public int? InternalErrorCode { get; set; }
        public string Message { get; set; }
    }
}