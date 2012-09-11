using System;

namespace datasift
{
    [Serializable()]
    public class AccessDeniedException : DataSiftException
    {
        public AccessDeniedException() : base() { }
        public AccessDeniedException(string message) : base(message) { }
        public AccessDeniedException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected AccessDeniedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
