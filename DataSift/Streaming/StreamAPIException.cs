using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSift.Streaming
{
    public class StreamAPIException : Exception
    {
        public StreamAPIException(string message, Exception exception)
            : base(message, exception) { }
    }
}
