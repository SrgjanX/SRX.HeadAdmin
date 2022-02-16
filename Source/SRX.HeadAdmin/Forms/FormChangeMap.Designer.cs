namespace SRX.HeadAdmin.Forms
{
    partial class FormChangeMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangeMap));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.buttonChangeMap = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboChooseMap = new System.Windows.Forms.ComboBox();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.buttonChangeMap);
            this.mainPanel.Controls.Add(this.buttonCancel);
            this.mainPanel.Controls.Add(this.comboChooseMap);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(283, 78);
            this.mainPanel.TabIndex = 0;
            // 
            // buttonChangeMap
            // 
            this.buttonChangeMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonChangeMap.Location = new System.Drawing.Point(145, 39);
            this.buttonChangeMap.Name = "buttonChangeMap";
            this.buttonChangeMap.Size = new System.Drawing.Size(127, 25);
            this.buttonChangeMap.TabIndex = 9;
            this.buttonChangeMap.Text = "Change Map";
            this.buttonChangeMap.UseVisualStyleBackColor = true;
            this.buttonChangeMap.Click += new System.EventHandler(this.buttonChangeMap_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Location = new System.Drawing.Point(12, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(127, 25);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboChooseMap
            // 
            this.comboChooseMap.BackColor = System.Drawing.Color.Black;
            this.comboChooseMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChooseMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboChooseMap.ForeColor = System.Drawing.Color.Green;
            this.comboChooseMap.FormattingEnabled = true;
            this.comboChooseMap.Location = new System.Drawing.Point(12, 12);
            this.comboChooseMap.Name = "comboChooseMap";
            this.comboChooseMap.Size = new System.Drawing.Size(260, 21);
            this.comboChooseMap.TabIndex = 0;
            // 
            // FormChangeMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(283, 78);
            this.Controls.Add(this.mainPanel);
            this.ForeColor = System.Drawing.Color.Green;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(283, 78);
            this.MinimumSize = new System.Drawing.Size(283, 78);
            this.Name = "FormChangeMap";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormChangeMap";
            this.Load += new System.EventHandler(this.FormChangeMap_Load);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.ComboBox comboChooseMap;
        private System.Windows.Forms.Button buttonChangeMap;
        private System.Windows.Forms.Button buttonCancel;
    }
}