using System;

namespace Exceptions
{
    public class ExceptionResponse
    {
        public int? InternalErrorCode { get; set; }
        public string Message { get; set; }

        public ExceptionResponse(Exception exception)
        {
            InternalErrorCode = exception is SolidamException solidamException ? (int?) solidamException.ErrorCode : null;
            Message = exception.Message;
        }
    }
}