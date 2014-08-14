using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocket4Net;

namespace DataSift.Streaming
{
    public interface IStreamConnection
    {
        event EventHandler Opened;
        event EventHandler Closed;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> Error;

        DateTime LastActiveTime
        {
            get;
        }

        void Open();

        void Close();

        void Reconnect();

        void Send(string message);
    }
}
