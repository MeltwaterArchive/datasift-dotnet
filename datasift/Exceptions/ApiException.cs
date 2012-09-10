using System;

namespace datasift
{
    [Serializable()]
    public class ApiException : DataSiftException
    {
        private int m_code = 0;
        public int Code
        {
            get
            {
                return m_code;
            }
        }

        public ApiException() : base() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, int code) : base(message) { m_code = code; }
        public ApiException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ApiException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
