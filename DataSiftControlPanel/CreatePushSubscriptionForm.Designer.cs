namespace DataSiftControlPanel
{
    // This is the new one
    partial class CreatePushSubscriptionForm
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
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            this.lblHash = new System.Windows.Forms.Label();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbHashType = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbInitialStatus = new System.Windows.Forms.ComboBox();
            this.cmbOutputType = new System.Windows.Forms.ComboBox();
            this.dgOutputParams = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgOutputParams)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHash
            // 
            this.lblHash.AutoSize = true;
            this.lblHash.Location = new System.Drawing.Point(12, 68);
            this.lblHash.Name = "lblHash";
            this.lblHash.Size = new System.Drawing.Size(66, 13);
            this.lblHash.TabIndex = 0;
            this.lblHash.Text = "Stream hash";
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
            label8.Location = new System.Drawing.Point(12, 41);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(55, 13);
            label8.TabIndex = 17;
            label8.Text = "Hash type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 94);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 13);
            label2.TabIndex = 19;
            label2.Text = "Initial status";
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(95, 65);
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(317, 20);
            this.txtHash.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(317, 20);
            this.txtName.TabIndex = 0;
            // 
            // cmbHashType
            // 
            this.cmbHashType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHashType.FormattingEnabled = true;
            this.cmbHashType.Items.AddRange(new object[] {
            "Live stream hash",
            "Historics playback ID"});
            this.cmbHashType.Location = new System.Drawing.Point(95, 38);
            this.cmbHashType.Name = "cmbHashType";
            this.cmbHashType.Size = new System.Drawing.Size(121, 21);
            this.cmbHashType.TabIndex = 1;
            this.cmbHashType.SelectedIndexChanged += new System.EventHandler(this.cmbHashType_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(247, 364);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(78, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cmbInitialStatus
            // 
            this.cmbInitialStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInitialStatus.FormattingEnabled = true;
            this.cmbInitialStatus.Items.AddRange(new object[] {
            "active",
            "paused",
            "stopped"});
            this.cmbInitialStatus.Location = new System.Drawing.Point(95, 91);
            this.cmbInitialStatus.Name = "cmbInitialStatus";
            this.cmbInitialStatus.Size = new System.Drawing.Size(121, 21);
            this.cmbInitialStatus.TabIndex = 3;
            // 
            // cmbOutputType
            // 
            this.cmbOutputType.FormattingEnabled = true;
            this.cmbOutputType.Items.AddRange(new object[] {
            "dynamodb",
            "ftp",
            "http",
            "mongodb",
            "s3"});
            this.cmbOutputType.Location = new System.Drawing.Point(95, 118);
            this.cmbOutputType.Name = "cmbOutputType";
            this.cmbOutputType.Size = new System.Drawing.Size(121, 21);
            this.cmbOutputType.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 121);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(62, 13);
            label3.TabIndex = 21;
            label3.Text = "Output type";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 149);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(76, 13);
            label4.TabIndex = 23;
            label4.Text = "Output params";
            // 
            // dgOutputParams
            // 
            this.dgOutputParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOutputParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colValue});
            this.dgOutputParams.Location = new System.Drawing.Point(95, 145);
            this.dgOutputParams.Name = "dgOutputParams";
            this.dgOutputParams.Size = new System.Drawing.Size(317, 213);
            this.dgOutputParams.TabIndex = 5;
            // 
            // colName
            // 
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            // 
            // CreatePushSubscriptionForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(424, 399);
            this.ControlBox = false;
            this.Controls.Add(this.dgOutputParams);
            this.Controls.Add(label4);
            this.Controls.Add(this.cmbOutputType);
            this.Controls.Add(label3);
            this.Controls.Add(this.cmbInitialStatus);
            this.Controls.Add(label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbHashType);
            this.Controls.Add(label8);
            this.Controls.Add(this.txtName);
            this.Controls.Add(label7);
            this.Controls.Add(this.txtHash);
            this.Controls.Add(this.lblHash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePushSubscriptionForm";
            this.Text = "Create a new Push subscription";
            ((System.ComponentModel.ISupportInitialize)(this.dgOutputParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbHashType;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbInitialStatus;
        private System.Windows.Forms.ComboBox cmbOutputType;
        private System.Windows.Forms.DataGridView dgOutputParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Label lblHash;

    }
}