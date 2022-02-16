//srgjanx

using SRX.HeadAdmin.Properties;
using SRX.HeadAdmin.Utils;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormBan : Form
    {
        public FormBan()
        {
            InitializeComponent();
        }

        private void FormBan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Settings.Default.Temp_BanTime = (int.Parse(txtHours.Text)*60) + int.Parse(txtMinutes.Text);
                Settings.Default.Temp_BanReason = txtReason.Text;
                if (radioAmxBan.Checked && !radioSSBan.Checked)
                    Program.banMethod = BanMethod.AmxBan;
                else if (!radioAmxBan.Checked && radioSSBan.Checked)
                    Program.banMethod = BanMethod.SSBan;
                Settings.Default.Temp_AllowBan = true;
                Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Settings.Default.Temp_BanTime = 0;
                Settings.Default.Temp_AllowBan = false;
                Program.banMethod = BanMethod.None;
                Close();
            }
        }

        private void txtHours_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMinutes_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}