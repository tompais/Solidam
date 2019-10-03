﻿using System;
using System.Runtime.Serialization;
using Enums;

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
    }
}