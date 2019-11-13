using System;
using System.Net;
using System.Runtime.Serialization;
using Enums;

namespace Exceptions
{
    [Serializable]
    public class SolidamException : Exception
    {
        public virtual ErrorCode ErrorCode { get; }
        public HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.InternalServerError;

        public SolidamException()
        {
        }

        public SolidamException(string message) : base(message)
        {
        }

        public SolidamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SolidamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}