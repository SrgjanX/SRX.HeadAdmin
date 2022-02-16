//srgjanx

using SRX.HeadAdmin.Properties;
using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormSlap : Form
    {
        public FormSlap()
        {
            InitializeComponent();
        }

        private void FormSlap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Settings.Default.Temp_SlapPower = int.Parse(txtValue.Text);
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Settings.Default.Temp_SlapPower = -1;
                Close();
            }
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtValue_Click(object sender, EventArgs e)
        {
            txtValue.Text = string.Empty;
        }
    }
}