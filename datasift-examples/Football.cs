using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using datasift;

namespace datasift_examples
{
    class Football : IEventHandler
    {
        private Thread m_thread = null;
        private Form1 m_form = null;
        private int m_count = 10;
        private StreamConsumer m_consumer = null;

        public Football(Form1 f)
        {
            m_form = f;
            m_thread = new Thread(new ThreadStart(run));
        }

        public void start()
        {
            m_thread.Start();
        }

        public void stop()
        {
            if (m_consumer != null && m_consumer.isRunning())
            {
                m_consumer.stop();
            }
        }

        public void run()
        {
            try
            {
                string csdl = "interaction.content contains \"football\"";

                m_form.footballLog("Creating definition...");
                m_form.footballLog("  " + csdl);
                Definition def = m_form.user.createDefinition(csdl);

                m_form.footballLog("Creating consumer...");
                m_consumer = def.getConsumer(this, "http");

                m_form.footballLog("Consuming...");
                m_consumer.consume();
            }
            catch (Exception e)
            {
                m_form.footballLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onConnect(StreamConsumer consumer)
        {
            m_form.footballLog("Connected");
            m_form.footballLog("--");
        }

        public void onInteraction(StreamConsumer consumer, Interaction interaction, string hash)
        {
            try
            {
                m_form.footballLog("Type: " + interaction.getStringVal("interaction.type"));
                m_form.footballLog("Content: " + interaction.getStringVal("interaction.content"));
                m_form.footballLog("--");

                m_count--;
                if (m_count == 0)
                {
                    m_form.footballLog("Stopping consumer...");
                    consumer.stop();
                }
            }
            catch (Exception e)
            {
                m_form.footballLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onDeleted(StreamConsumer consumer, Interaction interaction, string hash)
        {
            try
            {
                m_form.footballLog("Delete request for interaction " + interaction.getStringVal("interaction.id") + " of type " + interaction.getStringVal("interaction.type"));
                m_form.footballLog("Please delete it from your archive.");
                m_form.footballLog("--");
            }
            catch (Exception e)
            {
                m_form.footballLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onWarning(StreamConsumer consumer, string message)
        {
            m_form.footballLog("WARN: " + message);
            m_form.footballLog("--");
        }

        public void onError(StreamConsumer consumer, string message)
        {
            m_form.footballLog("ERR: " + message);
            m_form.footballLog("--");
        }

        public void onDisconnect(StreamConsumer consumer)
        {
            m_form.footballLog("Disconnected");
            m_form.footballLog("--");

            m_form.resetFootballStartButton();
        }
    }
}
