namespace SRX.HeadAdmin.Forms
{
    partial class FormUnban
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.radioIP = new System.Windows.Forms.RadioButton();
            this.radioSteamID = new System.Windows.Forms.RadioButton();
            this.buttonUnban = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblChooseMethod = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.radioIP);
            this.mainPanel.Controls.Add(this.radioSteamID);
            this.mainPanel.Controls.Add(this.buttonUnban);
            this.mainPanel.Controls.Add(this.buttonCancel);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.txtInput);
            this.mainPanel.Controls.Add(this.lblChooseMethod);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(426, 283);
            this.mainPanel.TabIndex = 0;
            // 
            // radioIP
            // 
            this.radioIP.AutoSize = true;
            this.radioIP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioIP.Location = new System.Drawing.Point(16, 109);
            this.radioIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioIP.Name = "radioIP";
            this.radioIP.Size = new System.Drawing.Size(182, 24);
            this.radioIP.TabIndex = 14;
            this.radioIP.Text = "Unban by IP Address";
            this.radioIP.UseVisualStyleBackColor = true;
            // 
            // radioSteamID
            // 
            this.radioSteamID.AutoSize = true;
            this.radioSteamID.Checked = true;
            this.radioSteamID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioSteamID.Location = new System.Drawing.Point(16, 74);
            this.radioSteamID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioSteamID.Name = "radioSteamID";
            this.radioSteamID.Size = new System.Drawing.Size(168, 24);
            this.radioSteamID.TabIndex = 13;
            this.radioSteamID.TabStop = true;
            this.radioSteamID.Text = "Unban by SteamID";
            this.radioSteamID.UseVisualStyleBackColor = true;
            // 
            // buttonUnban
            // 
            this.buttonUnban.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUnban.Location = new System.Drawing.Point(216, 229);
            this.buttonUnban.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUnban.Name = "buttonUnban";
            this.buttonUnban.Size = new System.Drawing.Size(190, 38);
            this.buttonUnban.TabIndex = 11;
            this.buttonUnban.Text = "Unban";
            this.buttonUnban.UseVisualStyleBackColor = true;
            this.buttonUnban.Click += new System.EventHandler(this.buttonUnban_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(16, 229);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(190, 38);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(16, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 44);
            this.label1.TabIndex = 6;
            this.label1.Text = "Enter SteamID or IP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.Color.Black;
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInput.ForeColor = System.Drawing.Color.Green;
            this.txtInput.Location = new System.Drawing.Point(16, 189);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInput.MaxLength = 512;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(389, 26);
            this.txtInput.TabIndex = 10;
            // 
            // lblChooseMethod
            // 
            this.lblChooseMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblChooseMethod.Location = new System.Drawing.Point(16, 12);
            this.lblChooseMethod.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblChooseMethod.Name = "lblChooseMethod";
            this.lblChooseMethod.Size = new System.Drawing.Size(389, 44);
            this.lblChooseMethod.TabIndex = 7;
            this.lblChooseMethod.Text = "Choose method for unbanning:";
            this.lblChooseMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormUnban
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(426, 283);
            this.ControlBox = false;
            this.Controls.Add(this.mainPanel);
            this.ForeColor = System.Drawing.Color.Green;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUnban";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button buttonUnban;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblChooseMethod;
        private System.Windows.Forms.RadioButton radioSteamID;
        private System.Windows.Forms.RadioButton radioIP;

    }
}