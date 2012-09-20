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
        delegate void AddOrUpdatePushItemCallback(PushSubscription p, ListViewItem lvi = null);
        delegate ListView.SelectedListViewItemCollection GetSelectedItemsCallback();
        delegate List<PushSubscription> GetSelectedPushSubscriptionsCallback();

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
            lstPushSubscriptions.BeginUpdate();
            initProgress("Fetching subscriptions...", 100);
            bwRefresh.RunWorkerAsync();
        }

        public void AddOrUpdatePushItem(PushSubscription p, ListViewItem lvi = null)
        {
            if (lstPushSubscriptions.InvokeRequired)
            {
                AddOrUpdatePushItemCallback cb = new AddOrUpdatePushItemCallback(AddOrUpdatePushItem);
                this.Invoke(cb, new object[] { p, lvi });
            }
            else
            {
                lstPushSubscriptions.BeginUpdate();

                try
                {
                    if (lvi == null)
                    {
                        // No ListViewItem given, try to find a matching row
                        foreach (ListViewItem lvi_candidate in lstPushSubscriptions.Items)
                        {
                            if (p.getId() == ((PushSubscription)lvi_candidate.Tag).getId())
                            {
                                lvi = lvi_candidate;
                                break;
                            }
                        }
                    }

                    if (p.isDeleted())
                    {
                        if (lvi != null)
                        {
                            lstPushSubscriptions.Items.Remove(lvi);
                        }
                    }
                    else
                    {
                        if (lvi == null)
                        {
                            // Still not found it, add it
                            lvi = lstPushSubscriptions.Items.Add(p.getName());
                            lvi.SubItems.Add(p.getCreatedAt().ToString());
                            lvi.SubItems.Add(p.getStatus());
                            lvi.SubItems.Add(p.getOutputType());
                            lvi.SubItems.Add(p.getLastRequest().ToString());
                            lvi.SubItems.Add(p.getLastSuccess().ToString());
                        }
                        else
                        {
                            // Already exists, update the pieces
                            lvi.SubItems[0].Text = p.getName();
                            lvi.SubItems[1].Text = p.getCreatedAt().ToString();
                            lvi.SubItems[2].Text = p.getStatus();
                            lvi.SubItems[3].Text = p.getOutputType();
                            lvi.SubItems[4].Text = p.getLastRequest().ToString();
                            lvi.SubItems[5].Text = p.getLastSuccess().ToString();
                        }

                        // Store the Push subscription in the item
                        lvi.Tag = p;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                lstPushSubscriptions.EndUpdate();
            }
        }

        public void SelectPushSubscription(string id)
        {
            foreach (ListViewItem lvi_candidate in lstPushSubscriptions.Items)
            {
                if (id == ((PushSubscription)lvi_candidate.Tag).getId())
                {
                    lstPushSubscriptions.SelectedItems.Clear();
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
                List<PushSubscription> subscriptions = user.listPushSubscriptions(1, 1000);
                if (subscriptions.Count > 0)
                {
                    bwRefresh.ReportProgress(50);
                    double progress_increment = 50 / subscriptions.Count;
                    double progress_current = 50.0;
                    foreach (PushSubscription p in subscriptions)
                    {
                        // Add or update it in the list
                        AddOrUpdatePushItem(p);
                        // Note that we've seen it
                        touched.Add(p.getId());
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
            foreach (ListViewItem lvi in lstPushSubscriptions.Items)
            {
                if (!touched.Contains(((PushSubscription)lvi.Tag).getId()))
                {
                    lstPushSubscriptions.Items.Remove(lvi);
                }
            }

            lstPushSubscriptions.EndUpdate();
            lstPushSubscriptions.Focus();
        }

        private ListView.SelectedListViewItemCollection GetSelectedItems()
        {
            if (lstPushSubscriptions.InvokeRequired)
            {
                GetSelectedItemsCallback cb = new GetSelectedItemsCallback(GetSelectedItems);
                return (ListView.SelectedListViewItemCollection)this.Invoke(cb);
            }
            else
            {
                return lstPushSubscriptions.SelectedItems;
            }
        }

        private List<PushSubscription> GetSelectedPushSubscriptions()
        {
            if (lstPushSubscriptions.InvokeRequired)
            {
                GetSelectedPushSubscriptionsCallback cb = new GetSelectedPushSubscriptionsCallback(GetSelectedPushSubscriptions);
                return (List<PushSubscription>)this.Invoke(cb);
            }
            else
            {
                List<PushSubscription> retval = new List<PushSubscription>();
                foreach (ListViewItem lvi in GetSelectedItems())
                {
                    retval.Add((PushSubscription)lvi.Tag);
                }
                return retval;
            }
        }

        private void mnuResume_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstPushSubscriptions.BeginUpdate();
            initProgress("Resuming subscription...", 100);
            bwDoOperation.RunWorkerAsync("resume");
        }

        private void mnuPause_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstPushSubscriptions.BeginUpdate();
            initProgress("Pausing subscription...", 100);
            bwDoOperation.RunWorkerAsync("pause");
        }

        private void mnuStop_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstPushSubscriptions.BeginUpdate();
            initProgress("Stopping subscription...", 100);
            bwDoOperation.RunWorkerAsync("stop");
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            Enabled = false;
            lstPushSubscriptions.BeginUpdate();
            initProgress("Deleting subscription...", 100);
            bwDoOperation.RunWorkerAsync("delete");
        }

        private void bwDoOperation_DoWork(object sender, DoWorkEventArgs e)
        {
            string operation = (string)e.Argument;
            List<PushSubscription> subscriptions = GetSelectedPushSubscriptions();
            double progress_increment = 50 / subscriptions.Count;
            double progress_current = 0.0;
            foreach (PushSubscription p in subscriptions)
            {
                try
                {
                    switch (operation)
                    {
                        case "resume":
                            p.resume();
                            e.Result = "";
                            break;

                        case "pause":
                            p.pause();
                            e.Result = "";
                            break;

                        case "stop":
                            p.stop();
                            e.Result = "";
                            break;

                        case "delete":
                            p.delete();
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

            foreach (PushSubscription p in subscriptions)
            {
                if (p.getStatus() != PushSubscription.STATUS_DELETED)
                {
                    p.reloadData();
                }
                AddOrUpdatePushItem(p);

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

            lstPushSubscriptions.EndUpdate();
            lstPushSubscriptions.Focus();
        }

        private void lstPushSubscriptions_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection lvis = GetSelectedItems();
            bool resume = true;
            bool pause = true;
            bool stop = true;
            bool delete = true;
            if (lvis.Count == 0)
            {
                resume = pause = stop = delete = false;
            }
            else foreach (ListViewItem lvi in GetSelectedItems())
            {
                PushSubscription p = (PushSubscription)lvi.Tag;
                switch (p.getStatus())
                {
                    case PushSubscription.STATUS_ACTIVE:
                        resume = false;
                        break;

                    case PushSubscription.STATUS_PAUSED:
                        pause = false;
                        break;

                    case PushSubscription.STATUS_STOPPED:
                        stop = false;
                        break;

                    case PushSubscription.STATUS_DELETED:
                        resume = false;
                        pause = false;
                        stop = false;
                        delete = false;
                        break;

                    case PushSubscription.STATUS_FINISHING:
                    case PushSubscription.STATUS_FINISHED:
                    case PushSubscription.STATUS_FAILED:
                    default:
                        resume = false;
                        pause = false;
                        stop = false;
                        break;
                }
            }

            mnuResume.Enabled = btnResume.Enabled = resume;
            mnuPause.Enabled = btnPause.Enabled = pause;
            mnuStop.Enabled = btnStop.Enabled = stop;
            mnuDelete.Enabled = btnDelete.Enabled = delete;
        }

        private void btnCreateNewPushSubscription_Click(object sender, EventArgs e)
        {
            (new CreatePushSubscriptionForm(this)).ShowDialog(this);
        }
    }
}
