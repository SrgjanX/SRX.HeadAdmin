//srgjanx

using SRX.HeadAdmin.Utils;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormUnban : Form
    {
        private string RegexPattern_SteamID = "^STEAM_0:0:([0-9]{5,10})$";
        private string RegexPattern_IP = "^[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}$";

        public FormUnban()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonUnban_Click(object sender, EventArgs e)
        {
            if (radioSteamID.Checked == false && radioIP.Checked == false)
            {
                MessageBox.Show("Please select unban method!", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (txtInput.Text == "")
            {
                MessageBox.Show("Please enter valid IP or SteamID", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (radioSteamID.Checked && !radioIP.Checked && Regex.IsMatch(txtInput.Text, RegexPattern_SteamID))
            {
                new Commands().UnbanPlayer(txtInput.Text);
                Logs.AppendLogs(LogsType.Ban, $"STEAMID: \"{txtInput.Text}\" has been unbanned!");
                MessageBox.Show($"Player with SteamID: '{txtInput.Text}' has been unbanned!", "Player Unbanned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!radioSteamID.Checked && radioIP.Checked && Regex.IsMatch(txtInput.Text, RegexPattern_IP))
            {
                new Commands().UnbanPlayer(txtInput.Text);
                Logs.AppendLogs(LogsType.Ban, $"IP: \"{txtInput.Text}\" has been unbanned!");
                MessageBox.Show($"Player with IP: '{txtInput.Text}' has been unbanned!", "Player Unbanned", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (radioSteamID.Checked && !radioIP.Checked)
                    MessageBox.Show("Invalid SteamID!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!radioSteamID.Checked && radioIP.Checked)
                    MessageBox.Show("Invalid IP!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (radioSteamID.Checked && !radioIP.Checked)
                    MessageBox.Show("Invalid SteamID or IP!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}