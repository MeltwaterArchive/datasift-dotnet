using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datasift;

namespace datasift_examples
{
    public partial class Form1 : Form
    {
        private User m_user = null;
        public User user
        {
            get { return m_user; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void tabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string api_key = txtAPIKey.Text.Trim();

            if (e.TabPageIndex != 0)
            {
                if (username.Length == 0 || api_key.Length == 0)
                {
                    MessageBox.Show("Please enter your DataSift username and API key.", "Error");
                    e.Cancel = true;
                    return;
                }
            }

            if (m_user == null || m_user.getUsername() != username || m_user.getApiKey() != api_key)
            {
                m_user = null;
                m_user = new User(username, api_key);
            }

            if (e.TabPage.Text.EndsWith("*"))
            {
                e.TabPage.Text = e.TabPage.Text.TrimEnd(new char[] { '*' });
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_football != null)
            {
                m_football.stop();
            }

            if (m_deletes != null)
            {
                m_deletes.stop();
            }
        }

        private delegate void appendTxtLogCallback(TextBox tb, string line, TabPage tp);
        private void appendTxtLog(TextBox tb, string txt, TabPage tp = null)
        {
            if (tb.InvokeRequired)
            {
                this.Invoke(new appendTxtLogCallback(appendTxtLog), new object[] { tb, txt, tp });
            }
            else
            {
                tb.AppendText(txt);
                if (tp != null && tp != tabs.SelectedTab && !tp.Text.EndsWith("*"))
                {
                    tp.Text += "*";
                }
            }
        }

        #region football
        private Football m_football = null;

        public void btnFootballStart_Click(object sender = null, EventArgs e = null)
        {
            if (btnFootballStart.Text == "Start")
            {
                btnFootballStart.Text = "Stop";
                m_football = new Football(this);
                m_football.start();
            }
            else
            {
                footballLog("Stopping...");
                m_football.stop();
            }

        }

        private delegate void resetFootballStartButtonCallback();
        public void resetFootballStartButton()
        {
            if (btnFootballStart.InvokeRequired)
            {
                this.Invoke(new resetFootballStartButtonCallback(resetFootballStartButton));
            }
            else
            {
                btnFootballStart.Text = "Start";
                m_football = null;
            }
        }

        public void footballLog(string log, bool addCR = true)
        {
            appendTxtLog(txtFootballLog, log + (addCR ? "\r\n" : ""), tabFootball);
        }
        #endregion

        #region deletes
        private Deletes m_deletes = null;

        public void btnDeletesStart_Click(object sender = null, EventArgs e = null)
        {
            if (btnDeletesStart.Text == "Start")
            {
                btnDeletesStart.Text = "Stop";
                m_deletes = null;
                m_deletes = new Deletes(this);
                m_deletes.start();
                deletesLog("\r\n");
            }
            else
            {
                deletesLog("\r\nStopping...");
                m_deletes.stop();
            }
        }

        private delegate void resetDeletesStartButtonCallback();
        public void resetDeletesStartButton()
        {
            if (btnDeletesStart.InvokeRequired)
            {
                this.Invoke(new resetDeletesStartButtonCallback(resetDeletesStartButton));
            }
            else
            {
                btnDeletesStart.Text = "Start";
                m_deletes = null;
            }
        }

        public void deletesLog(string log, bool addCR = true)
        {
            appendTxtLog(txtDeletesLog, log + (addCR ? "\r\n" : ""), tabDeletes);
        }
        #endregion
    }
}
