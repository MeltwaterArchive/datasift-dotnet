using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    public interface IEventHandler
    {
        void onConnect(StreamConsumer consumer);
        void onInteraction(StreamConsumer consumer, Interaction interaction, string hash);
        void onDeleted(StreamConsumer consumer, Interaction interaction, string hash);
        void onWarning(StreamConsumer consumer, string message);
        void onError(StreamConsumer consumer, string message);
        void onDisconnect(StreamConsumer consumer);
    }
}
