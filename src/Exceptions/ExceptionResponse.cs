using System;
using System.Linq.Expressions;
using Helpers;

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
                Message = Constant.DefaultUnhandledErrorMessage;
            }
        }

        public int? InternalErrorCode { get; set; }
        public string Message { get; set; }
    }
}