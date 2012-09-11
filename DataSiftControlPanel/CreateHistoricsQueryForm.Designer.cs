namespace DataSiftControlPanel
{
    partial class CreateHistoricsQueryForm
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            this.txtStreamHash = new System.Windows.Forms.TextBox();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.lbSourcesAvailable = new System.Windows.Forms.ListBox();
            this.lbSourcesSelected = new System.Windows.Forms.ListBox();
            this.btnSourceSelect = new System.Windows.Forms.Button();
            this.btnSourceDeselect = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbSampleRate = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 41);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(66, 13);
            label1.TabIndex = 0;
            label1.Text = "Stream hash";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 68);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 13);
            label2.TabIndex = 2;
            label2.Text = "Start date";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 94);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(50, 13);
            label3.TabIndex = 4;
            label3.Text = "End date";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 118);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(70, 13);
            label4.TabIndex = 8;
            label4.Text = "Data sources";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(92, 118);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(53, 13);
            label5.TabIndex = 10;
            label5.Text = "Available:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(263, 118);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 13);
            label6.TabIndex = 12;
            label6.Text = "Selected:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(12, 15);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(35, 13);
            label7.TabIndex = 15;
            label7.Text = "Name";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(12, 225);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(63, 13);
            label8.TabIndex = 17;
            label8.Text = "Sample rate";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(331, 68);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(29, 13);
            label9.TabIndex = 18;
            label9.Text = "UTC";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(331, 94);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(29, 13);
            label10.TabIndex = 19;
            label10.Text = "UTC";
            // 
            // txtStreamHash
            // 
            this.txtStreamHash.Location = new System.Drawing.Point(95, 38);
            this.txtStreamHash.Name = "txtStreamHash";
            this.txtStreamHash.Size = new System.Drawing.Size(317, 20);
            this.txtStreamHash.TabIndex = 1;
            // 
            // dtStartDate
            // 
            this.dtStartDate.CustomFormat = "ddd, MMMM dd, yyyy @ HH:mm:ss";
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartDate.Location = new System.Drawing.Point(95, 64);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(230, 20);
            this.dtStartDate.TabIndex = 2;
            // 
            // lbSourcesAvailable
            // 
            this.lbSourcesAvailable.FormattingEnabled = true;
            this.lbSourcesAvailable.Items.AddRange(new object[] {
            "2ch",
            "amazon",
            "blog",
            "board",
            "dailymotion",
            "facebook",
            "flickr",
            "imdb",
            "newscred",
            "reddit",
            "topix",
            "twitter",
            "video",
            "wikipedia",
            "youtube"});
            this.lbSourcesAvailable.Location = new System.Drawing.Point(95, 134);
            this.lbSourcesAvailable.Name = "lbSourcesAvailable";
            this.lbSourcesAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbSourcesAvailable.Size = new System.Drawing.Size(146, 82);
            this.lbSourcesAvailable.Sorted = true;
            this.lbSourcesAvailable.TabIndex = 6;
            // 
            // lbSourcesSelected
            // 
            this.lbSourcesSelected.FormattingEnabled = true;
            this.lbSourcesSelected.Location = new System.Drawing.Point(266, 134);
            this.lbSourcesSelected.Name = "lbSourcesSelected";
            this.lbSourcesSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbSourcesSelected.Size = new System.Drawing.Size(146, 82);
            this.lbSourcesSelected.Sorted = true;
            this.lbSourcesSelected.TabIndex = 9;
            // 
            // btnSourceSelect
            // 
            this.btnSourceSelect.Location = new System.Drawing.Point(243, 148);
            this.btnSourceSelect.Name = "btnSourceSelect";
            this.btnSourceSelect.Size = new System.Drawing.Size(20, 23);
            this.btnSourceSelect.TabIndex = 7;
            this.btnSourceSelect.Text = ">";
            this.btnSourceSelect.UseVisualStyleBackColor = true;
            this.btnSourceSelect.Click += new System.EventHandler(this.btnSourceSelect_Click);
            // 
            // btnSourceDeselect
            // 
            this.btnSourceDeselect.Location = new System.Drawing.Point(243, 177);
            this.btnSourceDeselect.Name = "btnSourceDeselect";
            this.btnSourceDeselect.Size = new System.Drawing.Size(20, 23);
            this.btnSourceDeselect.TabIndex = 8;
            this.btnSourceDeselect.Text = "<";
            this.btnSourceDeselect.UseVisualStyleBackColor = true;
            this.btnSourceDeselect.Click += new System.EventHandler(this.btnSourceDeselect_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(317, 20);
            this.txtName.TabIndex = 0;
            // 
            // cmbSampleRate
            // 
            this.cmbSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleRate.FormattingEnabled = true;
            this.cmbSampleRate.Items.AddRange(new object[] {
            "100%",
            "12.5%",
            "1.56%"});
            this.cmbSampleRate.Location = new System.Drawing.Point(95, 222);
            this.cmbSampleRate.Name = "cmbSampleRate";
            this.cmbSampleRate.Size = new System.Drawing.Size(71, 21);
            this.cmbSampleRate.TabIndex = 10;
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(247, 250);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(78, 23);
            this.btnCreate.TabIndex = 11;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dtEndDate
            // 
            this.dtEndDate.CustomFormat = "ddd, MMMM dd, yyyy @ HH:mm:ss";
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDate.Location = new System.Drawing.Point(95, 90);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(230, 20);
            this.dtEndDate.TabIndex = 4;
            // 
            // CreateHistoricsQueryForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 285);
            this.ControlBox = false;
            this.Controls.Add(label10);
            this.Controls.Add(label9);
            this.Controls.Add(this.dtEndDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbSampleRate);
            this.Controls.Add(label8);
            this.Controls.Add(this.txtName);
            this.Controls.Add(label7);
            this.Controls.Add(this.btnSourceDeselect);
            this.Controls.Add(this.btnSourceSelect);
            this.Controls.Add(label6);
            this.Controls.Add(this.lbSourcesSelected);
            this.Controls.Add(label5);
            this.Controls.Add(this.lbSourcesAvailable);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(this.dtStartDate);
            this.Controls.Add(label2);
            this.Controls.Add(this.txtStreamHash);
            this.Controls.Add(label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateHistoricsQueryForm";
            this.Text = "Create a new Historics query";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStreamHash;
        private System.Windows.Forms.DateTimePicker dtStartDate;
        private System.Windows.Forms.ListBox lbSourcesAvailable;
        private System.Windows.Forms.ListBox lbSourcesSelected;
        private System.Windows.Forms.Button btnSourceSelect;
        private System.Windows.Forms.Button btnSourceDeselect;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbSampleRate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DateTimePicker dtEndDate;

    }
}