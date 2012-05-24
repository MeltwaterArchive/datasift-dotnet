namespace datasift_examples
{
    partial class Form1
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label5;
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabWelcome = new System.Windows.Forms.TabPage();
            this.tabFootball = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnFootballStart = new System.Windows.Forms.Button();
            this.txtFootballLog = new System.Windows.Forms.TextBox();
            this.tabDeletes = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnDeletesStart = new System.Windows.Forms.Button();
            this.txtDeletesLog = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabWelcome.SuspendLayout();
            this.tabFootball.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabDeletes.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(873, 25);
            label2.TabIndex = 1;
            label2.Text = "In this example we subscribe to a stream that\'s looking for anything containing t" +
                "he word \"football.\" It waits for ten matching items and then stops.";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Controls.Add(this.txtAPIKey);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(this.txtUsername);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1022, 532);
            panel1.TabIndex = 0;
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Location = new System.Drawing.Point(70, 64);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(312, 20);
            this.txtAPIKey.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(9, 67);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(44, 13);
            label4.TabIndex = 3;
            label4.Text = "API key";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(70, 35);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(312, 20);
            this.txtUsername.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(9, 38);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(55, 13);
            label3.TabIndex = 1;
            label3.Text = "Username";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(377, 13);
            label1.TabIndex = 0;
            label1.Text = "Enter your DataSift username and API key - these will be used for all examples.";
            // 
            // label5
            // 
            label5.Dock = System.Windows.Forms.DockStyle.Fill;
            label5.Location = new System.Drawing.Point(0, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(873, 25);
            label5.TabIndex = 1;
            label5.Text = "This example consumes 1% of the Twitter stream until it\'s received 10 delete noti" +
                "fications. These must be properly handled to maintain compliance with the licens" +
                "e terms.";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabWelcome);
            this.tabs.Controls.Add(this.tabFootball);
            this.tabs.Controls.Add(this.tabDeletes);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1036, 564);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabs.TabIndex = 0;
            this.tabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabs_Selecting);
            // 
            // tabWelcome
            // 
            this.tabWelcome.Controls.Add(panel1);
            this.tabWelcome.Location = new System.Drawing.Point(4, 22);
            this.tabWelcome.Name = "tabWelcome";
            this.tabWelcome.Padding = new System.Windows.Forms.Padding(3);
            this.tabWelcome.Size = new System.Drawing.Size(1028, 538);
            this.tabWelcome.TabIndex = 0;
            this.tabWelcome.Text = "Welcome";
            this.tabWelcome.UseVisualStyleBackColor = true;
            // 
            // tabFootball
            // 
            this.tabFootball.Controls.Add(this.splitContainer1);
            this.tabFootball.Location = new System.Drawing.Point(4, 22);
            this.tabFootball.Name = "tabFootball";
            this.tabFootball.Padding = new System.Windows.Forms.Padding(3);
            this.tabFootball.Size = new System.Drawing.Size(1028, 538);
            this.tabFootball.TabIndex = 1;
            this.tabFootball.Text = "Football";
            this.tabFootball.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnFootballStart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtFootballLog);
            this.splitContainer1.Size = new System.Drawing.Size(1022, 532);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnFootballStart
            // 
            this.btnFootballStart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFootballStart.Location = new System.Drawing.Point(873, 0);
            this.btnFootballStart.Name = "btnFootballStart";
            this.btnFootballStart.Size = new System.Drawing.Size(149, 25);
            this.btnFootballStart.TabIndex = 0;
            this.btnFootballStart.Text = "Start";
            this.btnFootballStart.UseVisualStyleBackColor = true;
            this.btnFootballStart.Click += new System.EventHandler(this.btnFootballStart_Click);
            // 
            // txtFootballLog
            // 
            this.txtFootballLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtFootballLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFootballLog.Location = new System.Drawing.Point(0, 0);
            this.txtFootballLog.Multiline = true;
            this.txtFootballLog.Name = "txtFootballLog";
            this.txtFootballLog.ReadOnly = true;
            this.txtFootballLog.Size = new System.Drawing.Size(1022, 503);
            this.txtFootballLog.TabIndex = 1;
            this.txtFootballLog.TabStop = false;
            this.txtFootballLog.Text = "Click the \"Start\" button to run this example.";
            // 
            // tabDeletes
            // 
            this.tabDeletes.Controls.Add(this.splitContainer2);
            this.tabDeletes.Location = new System.Drawing.Point(4, 22);
            this.tabDeletes.Name = "tabDeletes";
            this.tabDeletes.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeletes.Size = new System.Drawing.Size(1028, 538);
            this.tabDeletes.TabIndex = 2;
            this.tabDeletes.Text = "Deletes";
            this.tabDeletes.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(label5);
            this.splitContainer2.Panel1.Controls.Add(this.btnDeletesStart);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtDeletesLog);
            this.splitContainer2.Size = new System.Drawing.Size(1022, 532);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // btnDeletesStart
            // 
            this.btnDeletesStart.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeletesStart.Location = new System.Drawing.Point(873, 0);
            this.btnDeletesStart.Name = "btnDeletesStart";
            this.btnDeletesStart.Size = new System.Drawing.Size(149, 25);
            this.btnDeletesStart.TabIndex = 0;
            this.btnDeletesStart.Text = "Start";
            this.btnDeletesStart.UseVisualStyleBackColor = true;
            this.btnDeletesStart.Click += new System.EventHandler(this.btnDeletesStart_Click);
            // 
            // txtDeletesLog
            // 
            this.txtDeletesLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtDeletesLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDeletesLog.Location = new System.Drawing.Point(0, 0);
            this.txtDeletesLog.Multiline = true;
            this.txtDeletesLog.Name = "txtDeletesLog";
            this.txtDeletesLog.ReadOnly = true;
            this.txtDeletesLog.Size = new System.Drawing.Size(1022, 503);
            this.txtDeletesLog.TabIndex = 0;
            this.txtDeletesLog.TabStop = false;
            this.txtDeletesLog.Text = "Click the \"Start\" button to run this example.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 564);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "DataSift API Examples";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabWelcome.ResumeLayout(false);
            this.tabFootball.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.tabDeletes.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabFootball;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnFootballStart;
        private System.Windows.Forms.TabPage tabWelcome;
        private System.Windows.Forms.TabPage tabDeletes;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnDeletesStart;
        private System.Windows.Forms.TextBox txtDeletesLog;
        private System.Windows.Forms.TextBox txtFootballLog;
    }
}

