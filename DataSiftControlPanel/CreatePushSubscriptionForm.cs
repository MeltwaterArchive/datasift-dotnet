using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datasift;

namespace DataSiftControlPanel
{
    // This is the new one!
    public partial class CreatePushSubscriptionForm : Form
    {
        private ListPushSubscriptionsForm m_frmPushSubscriptionsList = null;

        public CreatePushSubscriptionForm(ListPushSubscriptionsForm frmPushSubscriptionsList)
        {
            InitializeComponent();
            m_frmPushSubscriptionsList = frmPushSubscriptionsList;
            cmbHashType.SelectedIndex = 0;
            cmbInitialStatus.SelectedIndex = 0;
            cmbOutputType.Text = "http";
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;

            string errors = "";

            // Validate stuff
            if (txtName.Text.Trim().Length == 0)
            {
                errors += "Please enter a name for your new Historics query\n";
            }
            if (cmbHashType.Text.Trim().Length == 0)
            {
                errors += "Please specify a hash type\n";
            }
            if (txtHash.Text.Trim().Length == 0)
            {
                errors += "Please specify a hash\n";
            }
            if (cmbOutputType.Text.Trim().Length == 0)
            {
                errors += "Please specify an output type\n";
            }

            if (errors.Length > 0)
            {
                // The data is not good enough to try the API
                MessageBox.Show(this, errors, "Form errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Gather the data
                string name = txtName.Text.Trim();
                string hash_type = "hash";
                if (cmbHashType.Text.Trim() == "Historics playback ID")
                {
                    hash_type = "playback_id";
                }
                string hash = txtHash.Text.Trim();
                string initial_status = cmbInitialStatus.Text.Trim();
                string output_type = cmbOutputType.Text.Trim();

                // Get the user object and make the call
                User user = Program.Inst().dsuser;
                try
                {
                    PushDefinition pushdef = user.createPushDefinition();
                    pushdef.setOutputType(output_type);
                    foreach (DataGridViewRow row in dgOutputParams.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Length > 0)
                        {
                            pushdef.setOutputParam(row.Cells[0].Value.ToString(), row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString());
                        }
                    }

                    // Validate the output type and params
                    pushdef.validate();

                    PushSubscription pushsub = pushdef.subscribe(hash_type, hash, name);
                    if (m_frmPushSubscriptionsList != null)
                    {
                        m_frmPushSubscriptionsList.AddOrUpdatePushItem(pushsub);
                        m_frmPushSubscriptionsList.SelectPushSubscription(pushsub.getId());
                    }
                    else
                    {
                        MessageBox.Show(this, "Push subscription '" + pushsub.getId() + "' created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Close();
                }
                catch (DataSiftException ex)
                {
                    MessageBox.Show(this, ex.Message, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            UseWaitCursor = false;
            Enabled = true;
        }

        private void cmbHashType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHashType.Text == "Live stream hash")
            {
                lblHash.Text = "Stream hash";
            }
            else if (cmbHashType.Text == "Historics playback ID")
            {
                lblHash.Text = "Playback ID";
            }
        }
    }
}
