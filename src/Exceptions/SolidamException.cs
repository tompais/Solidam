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
    }
}