using System;
using System.Collections.Generic;
using System.Text;

namespace MessageService.Interfaces.Exceptions
{
    /// <summary>
    /// Custom Exception
    /// </summary>
    [Serializable()]
    public class CustomException : System.Exception
    {
        public CustomException() : base() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message, System.Exception inner) : base(message, inner) { }

        protected CustomException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Not Found Exception
    /// </summary>
    [Serializable()]
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, System.Exception inner) : base(message, inner) { }

        protected NotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    /// <summary>
    /// Unauthorized Exception
    /// </summary>
    [Serializable()]
    public class UnauthorizedException : System.Exception
    {
        public UnauthorizedException() : base() { }
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, System.Exception inner) : base(message, inner) { }
 
        protected UnauthorizedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
