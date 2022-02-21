//srgjanx

using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormSlap : Form
    {
        public delegate void SlapEventHandler(int slapPower);
        public event SlapEventHandler ShouldSlapPlayer;

        public FormSlap()
        {
            InitializeComponent();
        }

        private void FormSlap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int slapPower = int.Parse(txtValue.Text);
                if(slapPower > 0)
                {
                    ShouldSlapPlayer?.Invoke(slapPower);
                }
                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
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