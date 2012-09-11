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
    public partial class CreateHistoricsQueryForm : Form
    {
        private ListHistoricsQueriesForm m_frmHistoricsQueriesList = null;

        public CreateHistoricsQueryForm(ListHistoricsQueriesForm frmHistoricsQueriesList)
        {
            InitializeComponent();
            m_frmHistoricsQueriesList = frmHistoricsQueriesList;
        }

        private void btnSourceSelect_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (string item in lbSourcesAvailable.SelectedItems)
            {
                lbSourcesSelected.Items.Add(item);
                items.Add(item);
            }
            foreach (string item in items)
            {
                lbSourcesAvailable.Items.Remove(item);
            }
        }

        private void btnSourceDeselect_Click(object sender, EventArgs e)
        {
            List<string> items = new List<string>();
            foreach (string item in lbSourcesSelected.SelectedItems)
            {
                lbSourcesAvailable.Items.Add(item);
                items.Add(item);
            }
            foreach (string item in items)
            {
                lbSourcesSelected.Items.Remove(item);
            }
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
            if (txtStreamHash.Text.Trim().Length == 0)
            {
                errors += "Please specify a stream hash\n";
            }
            if (lbSourcesSelected.Items.Count == 0)
            {
                errors += "Please select one or more data sources\n";
            }
            if (cmbSampleRate.Text.Length == 0)
            {
                errors += "Please select a sample rate\n";
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
                string stream_hash = txtStreamHash.Text.Trim();
                DateTime start_date = dtStartDate.Value;
                DateTime end_date = dtEndDate.Value;
                List<string> sources = new List<string>();
                foreach (string item in lbSourcesSelected.Items)
                {
                    sources.Add(item.Trim());
                }
                double sample = Convert.ToDouble(cmbSampleRate.Text.Trim().Substring(0, cmbSampleRate.Text.Length - 1));

                // Get the user object and make the call
                User user = Program.Inst().dsuser;
                try
                {
                    Historic h = user.createHistoric(stream_hash, start_date, end_date, sources, sample, name);
                    h.prepare();
                    if (m_frmHistoricsQueriesList != null)
                    {
                        m_frmHistoricsQueriesList.AddOrUpdateHistoricItem(h);
                        m_frmHistoricsQueriesList.SelectHistoric(h.getHash());
                    }
                    else
                    {
                        MessageBox.Show(this, "Historic query '" + h.getHash() + "' created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
