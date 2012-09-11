using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSiftControlPanel
{
    public partial class AccountCredentialsForm : Form
    {
        public AccountCredentialsForm()
        {
            InitializeComponent();
        }

        private void AccountCredentialsForm_Shown(object sender, EventArgs e)
        {
            Program p = Program.Inst();
            txtUsername.Text = p.username;
            txtApiKey.Text = p.api_key;
            chkRemember.Checked = p.remember_credentials;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Program.Inst().UpdateCredentials(txtUsername.Text, txtApiKey.Text, chkRemember.Checked))
            {
                Close();
            }
            else
            {
                MessageBox.Show(this, "Invalid credentials", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
