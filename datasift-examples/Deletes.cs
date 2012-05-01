using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using datasift;

namespace datasift_examples
{
    class Deletes : IEventHandler
    {
        private Thread m_thread = null;
        private Form1 m_form = null;
        private int m_count = 10;
        private StreamConsumer m_consumer = null;

        public Deletes(Form1 f)
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
            if (m_consumer.isRunning())
            {
                m_consumer.stop();
            }
        }

        public void run()
        {
            try
            {
                string csdl = "interaction.type == \"twitter\" and interaction.sample <= 1.0";

                m_form.deletesLog("Creating definition...");
                m_form.deletesLog("  " + csdl);
                Definition def = m_form.user.createDefinition(csdl);

                m_form.deletesLog("Creating consumer...");
                m_consumer = def.getConsumer(this, "http");

                m_form.deletesLog("Consuming...");
                m_consumer.consume();
            }
            catch (Exception e)
            {
                m_form.deletesLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onConnect(StreamConsumer consumer)
        {
            m_form.deletesLog("Connected");
            m_form.deletesLog("--");
        }

        public void onInteraction(StreamConsumer consumer, Interaction interaction, string hash)
        {
            try
            {
                m_form.deletesLog(".", false);
            }
            catch (Exception e)
            {
                m_form.deletesLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onDeleted(StreamConsumer consumer, Interaction interaction, string hash)
        {
            try
            {
                m_form.deletesLog("X", false);

                m_count--;
                if (m_count == 0)
                {
                    m_form.deletesLog("\r\n\r\nStopping consumer...");
                    consumer.stop();
                }
            }
            catch (Exception e)
            {
                m_form.deletesLog(e.GetType().ToString() + ": " + e.Message);
            }
        }

        public void onWarning(StreamConsumer consumer, string message)
        {
            m_form.deletesLog("\r\nWARN: " + message);
        }

        public void onError(StreamConsumer consumer, string message)
        {
            m_form.deletesLog("\r\nERR: " + message);
        }

        public void onDisconnect(StreamConsumer consumer)
        {
            m_form.deletesLog("\r\nDisconnected");

            m_form.resetDeletesStartButton();
        }
    }
}
