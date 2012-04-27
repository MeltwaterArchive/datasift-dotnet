using System;

namespace datasift
{
    [Serializable()]
    public class CompileFailedException : Exception
    {
        public CompileFailedException() : base() { }
        public CompileFailedException(string message) : base(message) { }
        public CompileFailedException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected CompileFailedException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
