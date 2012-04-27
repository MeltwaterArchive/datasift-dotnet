using System;

namespace datasift
{
    [Serializable()]
    public class InvalidDataException : Exception
    {
        public InvalidDataException() : base() { }
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected InvalidDataException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
