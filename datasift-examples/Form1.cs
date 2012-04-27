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

        private Football m_football = null;

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
                m_user = new User(username, api_key);
            }
        }

        private delegate void lbAddCallback(ListBox lb, string line);
        private void lbAdd(ListBox lb, string line)
        {
            if (lb.InvokeRequired)
            {
                this.Invoke(new lbAddCallback(lbAdd), new object[] { lb, line });
            }
            else
            {
                lb.Items.Add(line);
                int idx = lb.Items.Count - 1;
                lb.SetSelected(idx, true);
                lb.SetSelected(idx, false);
            }
        }

        public void btnFootballStart_Click(object sender = null, EventArgs e = null)
        {
            btnFootballStart.Enabled = false;
            m_football = new Football(this);
            m_football.start();
        }

        private delegate void enableFootballStartButtonCallback();
        public void enableFootballStartButton()
        {
            if (btnFootballStart.InvokeRequired)
            {
                this.Invoke(new enableFootballStartButtonCallback(enableFootballStartButton));
            }
            else
            {
                btnFootballStart.Enabled = true;
            }
        }

        public void footballLog(string log)
        {
            lbAdd(lbFootball, log);
        }
    }
}
