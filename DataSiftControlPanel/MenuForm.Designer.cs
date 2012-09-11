namespace DataSiftControlPanel
{
    partial class frmMenu
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            this.btnHistoricsCreate = new System.Windows.Forms.Button();
            this.btnHistoricsList = new System.Windows.Forms.Button();
            this.btnPushCreate = new System.Windows.Forms.Button();
            this.btnPushList = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Controls.Add(this.btnHistoricsCreate);
            groupBox1.Controls.Add(this.btnHistoricsList);
            groupBox1.Location = new System.Drawing.Point(13, 42);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(159, 80);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Historics";
            // 
            // btnHistoricsCreate
            // 
            this.btnHistoricsCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoricsCreate.Location = new System.Drawing.Point(6, 48);
            this.btnHistoricsCreate.Name = "btnHistoricsCreate";
            this.btnHistoricsCreate.Size = new System.Drawing.Size(147, 23);
            this.btnHistoricsCreate.TabIndex = 3;
            this.btnHistoricsCreate.Text = "Create a new query";
            this.btnHistoricsCreate.UseVisualStyleBackColor = true;
            this.btnHistoricsCreate.Click += new System.EventHandler(this.btnHistoricsCreate_Click);
            // 
            // btnHistoricsList
            // 
            this.btnHistoricsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoricsList.Location = new System.Drawing.Point(6, 19);
            this.btnHistoricsList.Name = "btnHistoricsList";
            this.btnHistoricsList.Size = new System.Drawing.Size(147, 23);
            this.btnHistoricsList.TabIndex = 2;
            this.btnHistoricsList.Text = "List existing queries";
            this.btnHistoricsList.UseVisualStyleBackColor = true;
            this.btnHistoricsList.Click += new System.EventHandler(this.btnHistoricsList_Click);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(this.btnPushCreate);
            groupBox2.Controls.Add(this.btnPushList);
            groupBox2.Location = new System.Drawing.Point(12, 128);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(159, 80);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Push delivery";
            // 
            // btnPushCreate
            // 
            this.btnPushCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPushCreate.Location = new System.Drawing.Point(6, 48);
            this.btnPushCreate.Name = "btnPushCreate";
            this.btnPushCreate.Size = new System.Drawing.Size(147, 23);
            this.btnPushCreate.TabIndex = 3;
            this.btnPushCreate.Text = "Create a new subscription";
            this.btnPushCreate.UseVisualStyleBackColor = true;
            this.btnPushCreate.Click += new System.EventHandler(this.btnPushCreate_Click);
            // 
            // btnPushList
            // 
            this.btnPushList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPushList.Location = new System.Drawing.Point(6, 19);
            this.btnPushList.Name = "btnPushList";
            this.btnPushList.Size = new System.Drawing.Size(147, 23);
            this.btnPushList.TabIndex = 2;
            this.btnPushList.Text = "List existing subscriptions";
            this.btnPushList.UseVisualStyleBackColor = true;
            this.btnPushList.Click += new System.EventHandler(this.btnPushList_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccount.Location = new System.Drawing.Point(12, 12);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(160, 23);
            this.btnAccount.TabIndex = 0;
            this.btnAccount.Text = "DataSift Account";
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(13, 214);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 244);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.btnAccount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMenu";
            this.Text = "DataSift Control Panel";
            this.Shown += new System.EventHandler(this.frmMenu_Shown);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button btnHistoricsCreate;
        private System.Windows.Forms.Button btnHistoricsList;
        private System.Windows.Forms.Button btnPushCreate;
        private System.Windows.Forms.Button btnPushList;
        public System.Windows.Forms.Button btnExit;


    }
}

