using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace datasift
{
    /// <summary>
    /// This interface defines the methods that are required to receive events
    /// from a StreamConsumer object.
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// Called when a connection has been successfully established.
        /// </summary>
        /// <param name="consumer">The consumer object that has successfully connected.</param>
        void onConnect(StreamConsumer consumer);

        /// <summary>
        /// Called when an interaction is received.
        /// </summary>
        /// <param name="consumer">The consumer object that received the interaction.</param>
        /// <param name="interaction">The interaction itself.</param>
        /// <param name="hash">The stream hash which matched this interaction.</param>
        void onInteraction(StreamConsumer consumer, Interaction interaction, string hash);

        /// <summary>
        /// Called when a deletion notification is received.
        /// </summary>
        /// <param name="consumer">The consumer object that received the notification.</param>
        /// <param name="interaction">The notification itself.</param>
        /// <param name="hash">The stream hash which matched this interaction.</param>
        void onDeleted(StreamConsumer consumer, Interaction interaction, string hash);

        /// <summary>
        /// Called when a warning occurs. Warnings do not interrupt the
        /// stream connection.
        /// </summary>
        /// <param name="consumer">The consumer that raised or received the warning.</param>
        /// <param name="message">The warning message.</param>
        void onWarning(StreamConsumer consumer, string message);
        
        /// <summary>
        /// Called when an error occurs. The stream connection will usually be
        /// disconnected when this method returns.
        /// </summary>
        /// <param name="consumer">The consumer that raised or received the error.</param>
        /// <param name="message">The error message.</param>
        void onError(StreamConsumer consumer, string message);
        
        /// <summary>
        /// Called when the stream connection is disconnected.
        /// </summary>
        /// <param name="consumer">The consumer which was disconnected.</param>
        void onDisconnect(StreamConsumer consumer);
    }
}
