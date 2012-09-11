using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using datasift;
using System.Threading;

namespace DataSiftControlPanel
{
    public partial class ListPushSubscriptionsForm : Form
    {
        delegate void DisplayErrorCallback(string error);
        delegate void AddOrUpdateHistoricItemCallback(Historic h, ListViewItem lvi = null);
        delegate ListView.SelectedListViewItemCollection GetSelectedItemsCallback();
        delegate List<Historic> GetSelectedHistoricsCallback();

        public ListPushSubscriptionsForm()
        {
            InitializeComponent();
        }

        private void initProgress(string message, int num_items)
        {
            // Set up the progress bar
            progress.Minimum = 0;
            progress.Maximum = num_items;
            progress.Value = 0;
            // Set the label text
            lblOperation.Text = message;
            // Show both components
            progress.Visible = true;
            lblOperation.Visible = true;
        }

        private void DisplayError(string error)
        {
            if (InvokeRequired)
            {
                DisplayErrorCallback cb = new DisplayErrorCallback(DisplayError);
                this.Invoke(cb, new object[] { error });
            }
            else
            {
                MessageBox.Show(this, error, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void incProgress()
        {
            progress.Value++;
        }

        private void clearProgress()
        {
            // Hide both components
            progress.Visible = false;
            lblOperation.Visible = false;
        }

        private void ListPushSubscriptionsForm_Shown(object sender, EventArgs e)
        {
            btnRefresh_Click(btnRefresh, null);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstHistoricsQueries.BeginUpdate();
            initProgress("Fetching queries...", 100);
            bwRefresh.RunWorkerAsync();
        }

        public void AddOrUpdateHistoricItem(Historic h, ListViewItem lvi = null)
        {
            if (lstHistoricsQueries.InvokeRequired)
            {
                AddOrUpdateHistoricItemCallback cb = new AddOrUpdateHistoricItemCallback(AddOrUpdateHistoricItem);
                this.Invoke(cb, new object[] { h, lvi });
            }
            else
            {
                lstHistoricsQueries.BeginUpdate();

                try
                {
                    if (lvi == null)
                    {
                        // No ListViewItem given, try to find a matching row
                        foreach (ListViewItem lvi_candidate in lstHistoricsQueries.Items)
                        {
                            if (h.getHash() == ((Historic)lvi_candidate.Tag).getHash())
                            {
                                lvi = lvi_candidate;
                                break;
                            }
                        }
                    }

                    if (h.isDeleted())
                    {
                        if (lvi != null)
                        {
                            lstHistoricsQueries.Items.Remove(lvi);
                        }
                    }
                    else
                    {
                        if (lvi == null)
                        {
                            // Still not found it, add it
                            lvi = lstHistoricsQueries.Items.Add(h.getName());
                            lvi.SubItems.Add(h.getStatus());
                            lvi.SubItems.Add(h.getProgress().ToString());
                            lvi.SubItems.Add(h.getStartDate().ToString());
                            lvi.SubItems.Add(h.getEndDate().ToString());
                            lvi.SubItems.Add(string.Join(", ", h.getSources().ToArray()));
                        }
                        else
                        {
                            // Already exists, update the pieces
                            lvi.SubItems[0].Text = h.getName();
                            lvi.SubItems[1].Text = h.getStatus();
                            lvi.SubItems[2].Text = h.getProgress().ToString();
                            lvi.SubItems[3].Text = h.getStartDate().ToString();
                            lvi.SubItems[4].Text = h.getEndDate().ToString();
                            lvi.SubItems[5].Text = string.Join(", ", h.getSources().ToArray());
                        }

                        // Store the Historic in the item
                        lvi.Tag = h;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                lstHistoricsQueries.EndUpdate();
            }
        }

        public void SelectHistoric(string id)
        {
            foreach (ListViewItem lvi_candidate in lstHistoricsQueries.Items)
            {
                if (id == ((Historic)lvi_candidate.Tag).getHash())
                {
                    lstHistoricsQueries.SelectedItems.Clear();
                    lvi_candidate.Selected = true;
                }
            }
        }

        private void bwRefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> touched = new List<string>();
            try
            {
                User user = Program.Inst().dsuser;
                List<Historic> historics = user.listHistorics(1, 1000);
                if (historics.Count > 0)
                {
                    bwRefresh.ReportProgress(50);
                    double progress_increment = 50 / historics.Count;
                    double progress_current = 50.0;
                    foreach (Historic h in historics)
                    {
                        // Add or update it in the list
                        AddOrUpdateHistoricItem(h);
                        // Note that we've seen it
                        touched.Add(h.getHash());
                        // Update the progress
                        progress_current += progress_increment;
                        bwRefresh.ReportProgress((int)progress_current);
                    }
                }
            }
            catch (DataSiftException ex)
            {
                DisplayError(ex.Message);
            }
            e.Result = touched;
        }

        private void bwRefresh_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void bwRefresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UseWaitCursor = false;
            Enabled = true;
            clearProgress();

            // Remove any items we didn't see when we updated
            List<string> touched = (List<string>)e.Result;
            foreach (ListViewItem lvi in lstHistoricsQueries.Items)
            {
                if (!touched.Contains(((Historic)lvi.Tag).getHash()))
                {
                    lstHistoricsQueries.Items.Remove(lvi);
                }
            }

            lstHistoricsQueries.EndUpdate();
            lstHistoricsQueries.Focus();
        }

        private ListView.SelectedListViewItemCollection GetSelectedItems()
        {
            if (lstHistoricsQueries.InvokeRequired)
            {
                GetSelectedItemsCallback cb = new GetSelectedItemsCallback(GetSelectedItems);
                return (ListView.SelectedListViewItemCollection)this.Invoke(cb);
            }
            else
            {
                return lstHistoricsQueries.SelectedItems;
            }
        }

        private List<Historic> GetSelectedHistorics()
        {
            if (lstHistoricsQueries.InvokeRequired)
            {
                GetSelectedHistoricsCallback cb = new GetSelectedHistoricsCallback(GetSelectedHistorics);
                return (List<Historic>)this.Invoke(cb);
            }
            else
            {
                List<Historic> retval = new List<Historic>();
                foreach (ListViewItem lvi in GetSelectedItems())
                {
                    retval.Add((Historic)lvi.Tag);
                }
                return retval;
            }
        }

        private void mnuStart_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstHistoricsQueries.BeginUpdate();
            initProgress("Starting query...", 100);
            bwDoOperation.RunWorkerAsync("start");
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstHistoricsQueries.BeginUpdate();
            initProgress("Stopping query...", 100);
            bwDoOperation.RunWorkerAsync("stop");
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstHistoricsQueries.BeginUpdate();
            initProgress("Deleting query...", 100);
            bwDoOperation.RunWorkerAsync("delete");
        }

        private void bwDoOperation_DoWork(object sender, DoWorkEventArgs e)
        {
            string operation = (string)e.Argument;
            List<Historic> historics = GetSelectedHistorics();
            double progress_increment = 50 / historics.Count;
            double progress_current = 0.0;
            foreach (Historic h in historics)
            {
                try
                {
                    switch (operation)
                    {
                        case "start":
                            h.start();
                            e.Result = "";
                            break;

                        case "stop":
                            h.stop();
                            e.Result = "";
                            break;

                        case "delete":
                            h.delete();
                            e.Result = "";
                            break;

                        default:
                            e.Result = "Unknown operation: " + (string)e.Argument;
                            break;
                    }
                }
                catch (DataSiftException ex)
                {
                    e.Result = ex.Message;
                }

                if (((string)e.Result).Length > 0)
                {
                    return;
                }

                // Update the progress
                progress_current += progress_increment;
                bwDoOperation.ReportProgress((int)progress_current);
            }

            bwDoOperation.ReportProgress(50);

            Thread.Sleep(1000);

            foreach (Historic h in historics)
            {
                if (h.getStatus() != Historic.STATUS_DELETED)
                {
                    h.reloadData();
                }
                AddOrUpdateHistoricItem(h);

                // Update the progress
                progress_current += progress_increment;
                bwDoOperation.ReportProgress((int)progress_current);
            }
        }

        private void bwDoOperation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UseWaitCursor = false;
            Enabled = true;

            clearProgress();
            string result = (string)e.Result;
            if (result.Length > 0)
            {
                MessageBox.Show(this, result, "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lstHistoricsQueries.EndUpdate();
            lstHistoricsQueries.Focus();
        }

        private void lstHistoricsQueries_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection lvis = GetSelectedItems();
            bool start = true;
            bool stop = true;
            bool delete = true;
            if (lvis.Count == 0)
            {
                start = stop = delete = false;
            }
            else foreach (ListViewItem lvi in GetSelectedItems())
            {
                Historic h = (Historic)lvi.Tag;
                switch (h.getStatus())
                {
                    case "init":
                        stop = false;
                        break;

                    case "queued":
                    case "submitted":
                    case "prep":
                    case "running":
                    case "finishing":
                        start = false;
                        break;

                    case "finished":
                    case "succeeded":
                    default:
                        start = false;
                        stop = false;
                        break;
                }
            }

            mnuStart.Enabled = btnStart.Enabled = start;
            mnuStop.Enabled = btnStop.Enabled = stop;
            mnuDelete.Enabled = btnDelete.Enabled = delete;
        }

        private void btnCreateNewHistoricsQuery_Click(object sender, EventArgs e)
        {
            //(new CreateHistoricsQueryForm(this)).ShowDialog(this);
        }
    }
}
