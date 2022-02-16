namespace SRX.HeadAdmin.Forms
{
    partial class FormBan
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.radioSSBan = new System.Windows.Forms.RadioButton();
            this.radioAmxBan = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblBanMethod = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblReason = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblHours = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBanOptions = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.panel9);
            this.panelMain.Controls.Add(this.panel8);
            this.panelMain.Controls.Add(this.panel6);
            this.panelMain.Controls.Add(this.panel7);
            this.panelMain.Controls.Add(this.panel4);
            this.panelMain.Controls.Add(this.panel5);
            this.panelMain.Controls.Add(this.panel3);
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(375, 166);
            this.panelMain.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.radioSSBan);
            this.panel9.Controls.Add(this.radioAmxBan);
            this.panel9.Location = new System.Drawing.Point(131, 32);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(242, 34);
            this.panel9.TabIndex = 8;
            // 
            // radioSSBan
            // 
            this.radioSSBan.AutoSize = true;
            this.radioSSBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioSSBan.Location = new System.Drawing.Point(170, 9);
            this.radioSSBan.Name = "radioSSBan";
            this.radioSSBan.Size = new System.Drawing.Size(60, 17);
            this.radioSSBan.TabIndex = 1;
            this.radioSSBan.Text = "SS Ban";
            this.radioSSBan.UseVisualStyleBackColor = true;
            // 
            // radioAmxBan
            // 
            this.radioAmxBan.AutoSize = true;
            this.radioAmxBan.Checked = true;
            this.radioAmxBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioAmxBan.Location = new System.Drawing.Point(3, 9);
            this.radioAmxBan.Name = "radioAmxBan";
            this.radioAmxBan.Size = new System.Drawing.Size(75, 17);
            this.radioAmxBan.TabIndex = 0;
            this.radioAmxBan.TabStop = true;
            this.radioAmxBan.Text = "AMX_BAN";
            this.radioAmxBan.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.lblBanMethod);
            this.panel8.Location = new System.Drawing.Point(-1, 32);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(133, 34);
            this.panel8.TabIndex = 7;
            // 
            // lblBanMethod
            // 
            this.lblBanMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBanMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanMethod.Location = new System.Drawing.Point(0, 0);
            this.lblBanMethod.Name = "lblBanMethod";
            this.lblBanMethod.Size = new System.Drawing.Size(131, 32);
            this.lblBanMethod.TabIndex = 0;
            this.lblBanMethod.Text = "Ban Method:";
            this.lblBanMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblReason);
            this.panel6.Location = new System.Drawing.Point(-1, 131);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(134, 34);
            this.panel6.TabIndex = 6;
            // 
            // lblReason
            // 
            this.lblReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReason.Location = new System.Drawing.Point(0, 0);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(132, 32);
            this.lblReason.TabIndex = 0;
            this.lblReason.Text = "Reason:";
            this.lblReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.txtReason);
            this.panel7.Location = new System.Drawing.Point(132, 131);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(242, 34);
            this.panel7.TabIndex = 5;
            // 
            // txtReason
            // 
            this.txtReason.BackColor = System.Drawing.Color.Black;
            this.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.ForeColor = System.Drawing.Color.Green;
            this.txtReason.Location = new System.Drawing.Point(0, 0);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(240, 23);
            this.txtReason.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblMinutes);
            this.panel4.Location = new System.Drawing.Point(-1, 98);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(134, 34);
            this.panel4.TabIndex = 4;
            // 
            // lblMinutes
            // 
            this.lblMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinutes.Location = new System.Drawing.Point(0, 0);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(132, 32);
            this.lblMinutes.TabIndex = 0;
            this.lblMinutes.Text = "Minutes:";
            this.lblMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtMinutes);
            this.panel5.Location = new System.Drawing.Point(132, 98);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(242, 34);
            this.panel5.TabIndex = 3;
            // 
            // txtMinutes
            // 
            this.txtMinutes.BackColor = System.Drawing.Color.Black;
            this.txtMinutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutes.ForeColor = System.Drawing.Color.Green;
            this.txtMinutes.Location = new System.Drawing.Point(0, 0);
            this.txtMinutes.MaxLength = 2;
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(240, 23);
            this.txtMinutes.TabIndex = 0;
            this.txtMinutes.Text = "0";
            this.txtMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinutes_KeyPress);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblHours);
            this.panel3.Location = new System.Drawing.Point(-1, 65);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(134, 34);
            this.panel3.TabIndex = 2;
            // 
            // lblHours
            // 
            this.lblHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHours.Location = new System.Drawing.Point(0, 0);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(132, 32);
            this.lblHours.TabIndex = 0;
            this.lblHours.Text = "Hours:";
            this.lblHours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtHours);
            this.panel2.Location = new System.Drawing.Point(132, 65);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 34);
            this.panel2.TabIndex = 1;
            // 
            // txtHours
            // 
            this.txtHours.BackColor = System.Drawing.Color.Black;
            this.txtHours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHours.ForeColor = System.Drawing.Color.Green;
            this.txtHours.Location = new System.Drawing.Point(0, 0);
            this.txtHours.MaxLength = 3;
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(240, 23);
            this.txtHours.TabIndex = 0;
            this.txtHours.Text = "0";
            this.txtHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHours_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblBanOptions);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 34);
            this.panel1.TabIndex = 0;
            // 
            // lblBanOptions
            // 
            this.lblBanOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBanOptions.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanOptions.Location = new System.Drawing.Point(0, 0);
            this.lblBanOptions.Name = "lblBanOptions";
            this.lblBanOptions.Size = new System.Drawing.Size(373, 32);
            this.lblBanOptions.TabIndex = 0;
            this.lblBanOptions.Text = "Ban Options";
            this.lblBanOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(375, 166);
            this.Controls.Add(this.panelMain);
            this.ForeColor = System.Drawing.Color.Green;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormBan";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormBan_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBanOptions;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton radioSSBan;
        private System.Windows.Forms.RadioButton radioAmxBan;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblBanMethod;
    }
}