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
    public partial class frmMenu : Form
    {
        protected ListHistoricsQueriesForm frmListHistoricsQueries = null;
        protected ListPushSubscriptionsForm frmListPushSubscriptions = null;

        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Shown(object sender, EventArgs e)
        {
            if (Program.Inst().username.Length == 0)
            {
                (new AccountCredentialsForm()).ShowDialog(this);
            }
        }

        private void btnHistoricsList_Click(object sender, EventArgs e)
        {
            if (frmListHistoricsQueries == null || !frmListHistoricsQueries.Visible)
            {
                frmListHistoricsQueries = new ListHistoricsQueriesForm();
                frmListHistoricsQueries.Show(this);
            }
            else
            {
                frmListHistoricsQueries.Focus();
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            (new AccountCredentialsForm()).ShowDialog(this);
        }

        private void btnHistoricsCreate_Click(object sender, EventArgs e)
        {
            (new CreateHistoricsQueryForm(null)).ShowDialog(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPushList_Click(object sender, EventArgs e)
        {
            if (frmListPushSubscriptions == null || !frmListPushSubscriptions.Visible)
            {
                frmListPushSubscriptions = new ListPushSubscriptionsForm();
                frmListPushSubscriptions.Show(this);
            }
            else
            {
                frmListPushSubscriptions.Focus();
            }
        }

        private void btnPushCreate_Click(object sender, EventArgs e)
        {
            (new CreatePushSubscriptionForm(null)).ShowDialog(this);
        }
    }
}
