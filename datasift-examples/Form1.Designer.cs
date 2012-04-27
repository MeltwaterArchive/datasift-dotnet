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
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnFootballStart = new System.Windows.Forms.Button();
            this.lbFootball = new System.Windows.Forms.ListBox();
            label2 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panel1.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1036, 564);
            this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabs.TabIndex = 0;
            this.tabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabs_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1028, 538);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Welcome";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1028, 538);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Football";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel2.Controls.Add(this.lbFootball);
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
            // lbFootball
            // 
            this.lbFootball.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFootball.FormattingEnabled = true;
            this.lbFootball.Items.AddRange(new object[] {
            "Click the \"Start\" button to connect to run this example."});
            this.lbFootball.Location = new System.Drawing.Point(0, 0);
            this.lbFootball.Name = "lbFootball";
            this.lbFootball.Size = new System.Drawing.Size(1022, 503);
            this.lbFootball.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 564);
            this.Controls.Add(this.tabs);
            this.Name = "Form1";
            this.Text = "DataSift API Examples";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnFootballStart;
        private System.Windows.Forms.ListBox lbFootball;
        private System.Windows.Forms.TabPage tabPage1;
    }
}

