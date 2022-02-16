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

        private void checkSteamID_CheckedChanged(object sender, EventArgs e)
        {
            radioSteamID.Checked = true;
            radioIP.Checked = false;
        }

        private void checkIP_CheckedChanged(object sender, EventArgs e)
        {
            radioSteamID.Checked = false;
            radioIP.Checked = true;
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
            else if (radioSteamID.Checked && !radioIP.Checked && Regex.IsMatch(txtInput.Text, RegexPattern_SteamID))
            {
                Commands.SendRCON("amx_unban \"" + txtInput.Text + "\"");
                Logs.AppendLogs(LogsType.BanLogs, "STEAMID: \"" + txtInput.Text + "\" has been unbanned!");
                MessageBox.Show("Player with SteamID: '"+txtInput.Text+"' has been unbanned!","Unban done",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (!radioSteamID.Checked && radioIP.Checked && Regex.IsMatch(txtInput.Text, RegexPattern_IP))
            {
                Commands.SendRCON("amx_unban \"" + txtInput.Text+"\"");
                Logs.AppendLogs(LogsType.BanLogs, "IP: \"" + txtInput.Text + "\" has been unbanned!");
                MessageBox.Show("Player with IP: '" + txtInput.Text + "' has been unbanned!", "Unban done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (radioSteamID.Checked && !radioIP.Checked)
                    MessageBox.Show("Invalid SteamID!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (!radioSteamID.Checked && radioIP.Checked)
                    MessageBox.Show("Invalid IP!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (radioSteamID.Checked && !radioIP.Checked)
                    MessageBox.Show("Invalid SteamID or IP!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("The user with steamid/ip: '"+txtInput.Text+"' can not be found on banlist!", "Invalid Attempt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}