using DataSift;
using DataSift.Enum;
using DataSift.Streaming;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataSiftTests
{
    internal class MockStreamConnection : IStreamConnection
    {
        public event EventHandler Opened;
        public event EventHandler Closed;
        public event EventHandler<WebSocket4Net.MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<SuperSocket.ClientEngine.ErrorEventArgs> Error;

        public DateTime LastActiveTime
        {
            get { return DateTime.Now;  }
        }

        public MockStreamConnection(string url) {
            Timer timer = new Timer(1000);
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// Generates a fake interaction every second for testing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            dynamic fakeInteraction = new { data = new { interaction = new { content = "Test content"} }, hash = "b09z345fe2f1fed748c12268fd473662" };
            MessageReceived(this, new WebSocket4Net.MessageReceivedEventArgs(JsonConvert.SerializeObject(fakeInteraction)));
        }

        public void Reconnect()
        {}

        public void Close() { }

        public void Open()
        {
            if (Opened != null)
                Opened(this, new EventArgs());
        }

        public void Send(string message)
        {
            var msg = APIHelpers.DeserializeResponse(message);
            dynamic response = null;

            if(msg.action == "subscribe")
            { 
                // Fake subscription success
                response = new { status = APIHelpers.GetEnumDescription(DataSiftMessageStatus.Success), hash = msg.hash };
            }

            MessageReceived(this, new WebSocket4Net.MessageReceivedEventArgs( JsonConvert.SerializeObject(response)) );

        }
    }
}
