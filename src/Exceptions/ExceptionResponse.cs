using System;

namespace Exceptions
{
    public class ExceptionResponse
    {
        public ExceptionResponse(Exception exception)
        {
            InternalErrorCode = exception is SolidamException solidamException
                ? (int?) solidamException.ErrorCode
                : null;
            Message = exception.Message;
        }

        public int? InternalErrorCode { get; set; }
        public string Message { get; set; }
    }
}