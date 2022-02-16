//srgjanx

using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormExit : Form
    {
        public FormExit()
        {
            InitializeComponent();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}