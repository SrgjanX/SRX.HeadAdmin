//srgjanx

using SRX.HeadAdmin.Utils;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormBan : Form
    {
        public delegate void BanEventHandler(BanMethod banMethod, int banTime, string banReason);
        public event BanEventHandler ShouldBanPlayer;

        public FormBan()
        {
            InitializeComponent();
        }

        private void FormBan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BanMethod banMethod = BanMethod.AmxBan;
                int banTime = (int.Parse(txtHours.Text) * 60) + int.Parse(txtMinutes.Text);
                string banReason = txtReason.Text;
                if (radioAmxBan.Checked && !radioSSBan.Checked)
                    banMethod = BanMethod.AmxBan;
                else if (!radioAmxBan.Checked && radioSSBan.Checked)
                    banMethod = BanMethod.SSBan;
                ShouldBanPlayer?.Invoke(banMethod, banTime, banReason);
                Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
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