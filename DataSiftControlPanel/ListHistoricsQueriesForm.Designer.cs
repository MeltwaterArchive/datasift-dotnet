namespace DataSiftControlPanel
{
    partial class ListHistoricsQueriesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListHistoricsQueriesForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCreateNewHistoricsQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblOperation = new System.Windows.Forms.ToolStripLabel();
            this.lstHistoricsQueries = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSources = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.bwRefresh = new System.ComponentModel.BackgroundWorker();
            this.bwDoOperation = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            this.ctxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateNewHistoricsQuery,
            this.toolStripSeparator1,
            this.btnStart,
            this.btnStop,
            this.btnDelete,
            this.toolStripSeparator2,
            this.btnRefresh,
            this.progress,
            this.lblOperation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(753, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCreateNewHistoricsQuery
            // 
            this.btnCreateNewHistoricsQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateNewHistoricsQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateNewHistoricsQuery.Image")));
            this.btnCreateNewHistoricsQuery.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnCreateNewHistoricsQuery.Name = "btnCreateNewHistoricsQuery";
            this.btnCreateNewHistoricsQuery.Size = new System.Drawing.Size(23, 22);
            this.btnCreateNewHistoricsQuery.Text = "Create a new Historics query...";
            this.btnCreateNewHistoricsQuery.Click += new System.EventHandler(this.btnCreateNewHistoricsQuery_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStart.Enabled = false;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(23, 22);
            this.btnStart.Text = "toolStripButton1";
            this.btnStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "toolStripButton2";
            this.btnStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23, 22);
            this.btnDelete.Text = "toolStripButton3";
            this.btnDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // progress
            // 
            this.progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 22);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progress.ToolTipText = "Operation progress";
            this.progress.Visible = false;
            // 
            // lblOperation
            // 
            this.lblOperation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(66, 22);
            this.lblOperation.Text = "Initialising...";
            this.lblOperation.Visible = false;
            // 
            // lstHistoricsQueries
            // 
            this.lstHistoricsQueries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colStatus,
            this.colProgress,
            this.colStart,
            this.colEnd,
            this.colSources});
            this.lstHistoricsQueries.ContextMenuStrip = this.ctxMenu;
            this.lstHistoricsQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstHistoricsQueries.FullRowSelect = true;
            this.lstHistoricsQueries.GridLines = true;
            this.lstHistoricsQueries.Location = new System.Drawing.Point(0, 25);
            this.lstHistoricsQueries.Name = "lstHistoricsQueries";
            this.lstHistoricsQueries.Size = new System.Drawing.Size(753, 413);
            this.lstHistoricsQueries.TabIndex = 1;
            this.lstHistoricsQueries.UseCompatibleStateImageBehavior = false;
            this.lstHistoricsQueries.View = System.Windows.Forms.View.Details;
            this.lstHistoricsQueries.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstHistoricsQueries_ItemSelectionChanged);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 216;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 75;
            // 
            // colProgress
            // 
            this.colProgress.Text = "% done";
            this.colProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colProgress.Width = 50;
            // 
            // colStart
            // 
            this.colStart.Text = "Start";
            this.colStart.Width = 120;
            // 
            // colEnd
            // 
            this.colEnd.Text = "End";
            this.colEnd.Width = 120;
            // 
            // colSources
            // 
            this.colSources.Text = "Sources";
            this.colSources.Width = 150;
            // 
            // ctxMenu
            // 
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuStop,
            this.mnuDelete});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(117, 70);
            // 
            // mnuStart
            // 
            this.mnuStart.Image = ((System.Drawing.Image)(resources.GetObject("mnuStart.Image")));
            this.mnuStart.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Size = new System.Drawing.Size(116, 22);
            this.mnuStart.Text = "&Start";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuStop.Image")));
            this.mnuStop.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(116, 22);
            this.mnuStop.Text = "S&top";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Image = ((System.Drawing.Image)(resources.GetObject("mnuDelete.Image")));
            this.mnuDelete.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(116, 22);
            this.mnuDelete.Text = "&Delete";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // bwRefresh
            // 
            this.bwRefresh.WorkerReportsProgress = true;
            this.bwRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRefresh_DoWork);
            this.bwRefresh.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRefresh_ProgressChanged);
            this.bwRefresh.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRefresh_RunWorkerCompleted);
            // 
            // bwDoOperation
            // 
            this.bwDoOperation.WorkerReportsProgress = true;
            this.bwDoOperation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwDoOperation_DoWork);
            this.bwDoOperation.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRefresh_ProgressChanged);
            this.bwDoOperation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwDoOperation_RunWorkerCompleted);
            // 
            // ListHistoricsQueriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 438);
            this.Controls.Add(this.lstHistoricsQueries);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ListHistoricsQueriesForm";
            this.Text = "Historics queries";
            this.Shown += new System.EventHandler(this.ListHistoricsQueriesForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCreateNewHistoricsQuery;
        private System.Windows.Forms.ListView lstHistoricsQueries;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colProgress;
        private System.Windows.Forms.ColumnHeader colStart;
        private System.Windows.Forms.ColumnHeader colEnd;
        private System.Windows.Forms.ColumnHeader colSources;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ToolStripLabel lblOperation;
        private System.ComponentModel.BackgroundWorker bwRefresh;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuStart;
        private System.Windows.Forms.ToolStripMenuItem mnuStop;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.ComponentModel.BackgroundWorker bwDoOperation;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}