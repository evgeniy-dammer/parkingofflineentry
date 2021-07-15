namespace ParkingOfflineEntry
{
    partial class ParkingOfflineEntryMain
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
            this.comboBoxNationalityType = new System.Windows.Forms.ComboBox();
            this.comboBoxCarType = new System.Windows.Forms.ComboBox();
            this.ButtonPrint = new System.Windows.Forms.Button();
            this.labelCarplate = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.printDialogQRCode = new System.Windows.Forms.PrintDialog();
            this.printDocumentQRCode = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDatabaseHost = new System.Windows.Forms.TextBox();
            this.textBoxDatabasePort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDatabaseName = new System.Windows.Forms.TextBox();
            this.textBoxDatabaseUser = new System.Windows.Forms.TextBox();
            this.textBoxDatabasePass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxParkingId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxBarrierComPort = new System.Windows.Forms.TextBox();
            this.buttonCloseBarrier = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxBarrierId = new System.Windows.Forms.TextBox();
            this.textBoxSensorsComPort = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxNationalityType
            // 
            this.comboBoxNationalityType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNationalityType.Location = new System.Drawing.Point(235, 38);
            this.comboBoxNationalityType.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxNationalityType.Name = "comboBoxNationalityType";
            this.comboBoxNationalityType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxNationalityType.TabIndex = 4;
            // 
            // comboBoxCarType
            // 
            this.comboBoxCarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCarType.ItemHeight = 13;
            this.comboBoxCarType.Location = new System.Drawing.Point(235, 13);
            this.comboBoxCarType.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCarType.Name = "comboBoxCarType";
            this.comboBoxCarType.Size = new System.Drawing.Size(200, 21);
            this.comboBoxCarType.TabIndex = 3;
            // 
            // ButtonPrint
            // 
            this.ButtonPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonPrint.Location = new System.Drawing.Point(235, 62);
            this.ButtonPrint.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonPrint.Name = "ButtonPrint";
            this.ButtonPrint.Size = new System.Drawing.Size(200, 44);
            this.ButtonPrint.TabIndex = 1;
            this.ButtonPrint.Text = "Print ticket";
            this.ButtonPrint.UseVisualStyleBackColor = true;
            this.ButtonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            // 
            // labelCarplate
            // 
            this.labelCarplate.AutoSize = true;
            this.labelCarplate.Location = new System.Drawing.Point(225, 164);
            this.labelCarplate.Name = "labelCarplate";
            this.labelCarplate.Size = new System.Drawing.Size(0, 13);
            this.labelCarplate.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(439, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // printDialogQRCode
            // 
            this.printDialogQRCode.UseEXDialog = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Database Host:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Database Port:";
            // 
            // textBoxDatabaseHost
            // 
            this.textBoxDatabaseHost.Location = new System.Drawing.Point(112, 37);
            this.textBoxDatabaseHost.Name = "textBoxDatabaseHost";
            this.textBoxDatabaseHost.Size = new System.Drawing.Size(119, 20);
            this.textBoxDatabaseHost.TabIndex = 6;
            this.textBoxDatabaseHost.TextChanged += new System.EventHandler(this.TextBoxDatabaseHost_TextChanged);
            // 
            // textBoxDatabasePort
            // 
            this.textBoxDatabasePort.Location = new System.Drawing.Point(112, 61);
            this.textBoxDatabasePort.Name = "textBoxDatabasePort";
            this.textBoxDatabasePort.Size = new System.Drawing.Size(119, 20);
            this.textBoxDatabasePort.TabIndex = 7;
            this.textBoxDatabasePort.TextChanged += new System.EventHandler(this.TextBoxDatabasePort_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Database Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Database User:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Database Pass:";
            // 
            // textBoxDatabaseName
            // 
            this.textBoxDatabaseName.Location = new System.Drawing.Point(112, 86);
            this.textBoxDatabaseName.Name = "textBoxDatabaseName";
            this.textBoxDatabaseName.Size = new System.Drawing.Size(119, 20);
            this.textBoxDatabaseName.TabIndex = 8;
            this.textBoxDatabaseName.TextChanged += new System.EventHandler(this.TextBoxDatabaseName_TextChanged);
            // 
            // textBoxDatabaseUser
            // 
            this.textBoxDatabaseUser.Location = new System.Drawing.Point(112, 110);
            this.textBoxDatabaseUser.Name = "textBoxDatabaseUser";
            this.textBoxDatabaseUser.Size = new System.Drawing.Size(119, 20);
            this.textBoxDatabaseUser.TabIndex = 9;
            this.textBoxDatabaseUser.TextChanged += new System.EventHandler(this.TextBoxDatabaseUser_TextChanged);
            // 
            // textBoxDatabasePass
            // 
            this.textBoxDatabasePass.Location = new System.Drawing.Point(112, 135);
            this.textBoxDatabasePass.Name = "textBoxDatabasePass";
            this.textBoxDatabasePass.PasswordChar = '*';
            this.textBoxDatabasePass.Size = new System.Drawing.Size(119, 20);
            this.textBoxDatabasePass.TabIndex = 10;
            this.textBoxDatabasePass.TextChanged += new System.EventHandler(this.TextBoxDatabasePass_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Parking ID:";
            // 
            // textBoxParkingId
            // 
            this.textBoxParkingId.Location = new System.Drawing.Point(112, 13);
            this.textBoxParkingId.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxParkingId.Name = "textBoxParkingId";
            this.textBoxParkingId.Size = new System.Drawing.Size(119, 20);
            this.textBoxParkingId.TabIndex = 5;
            this.textBoxParkingId.TextChanged += new System.EventHandler(this.TextBoxParkingId_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Barrier COM Port:";
            // 
            // textBoxBarrierComPort
            // 
            this.textBoxBarrierComPort.Location = new System.Drawing.Point(112, 161);
            this.textBoxBarrierComPort.Name = "textBoxBarrierComPort";
            this.textBoxBarrierComPort.Size = new System.Drawing.Size(119, 20);
            this.textBoxBarrierComPort.TabIndex = 11;
            this.textBoxBarrierComPort.TextChanged += new System.EventHandler(this.TextBoxBarrierComPort_TextChanged);
            // 
            // buttonCloseBarrier
            // 
            this.buttonCloseBarrier.Location = new System.Drawing.Point(235, 111);
            this.buttonCloseBarrier.Name = "buttonCloseBarrier";
            this.buttonCloseBarrier.Size = new System.Drawing.Size(200, 45);
            this.buttonCloseBarrier.TabIndex = 2;
            this.buttonCloseBarrier.Text = "Close barrier";
            this.buttonCloseBarrier.UseVisualStyleBackColor = true;
            this.buttonCloseBarrier.Click += new System.EventHandler(this.ButtonCloseBarrier_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Barrier ID:";
            // 
            // textBoxBarrierId
            // 
            this.textBoxBarrierId.Location = new System.Drawing.Point(112, 213);
            this.textBoxBarrierId.Name = "textBoxBarrierId";
            this.textBoxBarrierId.Size = new System.Drawing.Size(119, 20);
            this.textBoxBarrierId.TabIndex = 13;
            this.textBoxBarrierId.TextChanged += new System.EventHandler(this.TextBoxBarrierId_TextChanged);
            // 
            // textBoxSensorsComPort
            // 
            this.textBoxSensorsComPort.Location = new System.Drawing.Point(112, 187);
            this.textBoxSensorsComPort.Name = "textBoxSensorsComPort";
            this.textBoxSensorsComPort.Size = new System.Drawing.Size(119, 20);
            this.textBoxSensorsComPort.TabIndex = 12;
            this.textBoxSensorsComPort.TextChanged += new System.EventHandler(this.TextBoxSensorsComPort_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Sensors COM Port:";
            // 
            // ParkingOfflineEntryMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 246);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxSensorsComPort);
            this.Controls.Add(this.textBoxBarrierId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonCloseBarrier);
            this.Controls.Add(this.textBoxBarrierComPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxParkingId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxDatabasePass);
            this.Controls.Add(this.textBoxDatabaseUser);
            this.Controls.Add(this.textBoxDatabaseName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDatabasePort);
            this.Controls.Add(this.textBoxDatabaseHost);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelCarplate);
            this.Controls.Add(this.comboBoxNationalityType);
            this.Controls.Add(this.comboBoxCarType);
            this.Controls.Add(this.ButtonPrint);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(465, 285);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(465, 285);
            this.Name = "ParkingOfflineEntryMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parking Offline Entry";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnterPressed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxNationalityType;
        private System.Windows.Forms.ComboBox comboBoxCarType;
        private System.Windows.Forms.Button ButtonPrint;
        private System.Windows.Forms.Label labelCarplate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PrintDialog printDialogQRCode;
        private System.Drawing.Printing.PrintDocument printDocumentQRCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDatabaseHost;
        private System.Windows.Forms.TextBox textBoxDatabasePort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDatabaseName;
        private System.Windows.Forms.TextBox textBoxDatabaseUser;
        private System.Windows.Forms.TextBox textBoxDatabasePass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxParkingId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxBarrierComPort;
        private System.Windows.Forms.Button buttonCloseBarrier;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxBarrierId;
        private System.Windows.Forms.TextBox textBoxSensorsComPort;
        private System.Windows.Forms.Label label9;
    }
}

