using System;

namespace datasift
{
    [Serializable()]
    public class DataSiftException : Exception
    {
        public DataSiftException() : base() { }
        public DataSiftException(string message) : base(message) { }
        public DataSiftException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected DataSiftException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
