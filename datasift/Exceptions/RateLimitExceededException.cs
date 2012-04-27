using System;

namespace datasift
{
    [Serializable()]
    public class RateLimitExceededException : Exception
    {
        public RateLimitExceededException() : base() { }
        public RateLimitExceededException(string message) : base(message) { }
        public RateLimitExceededException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected RateLimitExceededException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
