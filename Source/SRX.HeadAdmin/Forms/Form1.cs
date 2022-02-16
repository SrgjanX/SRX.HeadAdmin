//srgjanx

using SRX.HeadAdmin.Properties;
using SRX.HeadAdmin.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class Form1 : Form
    {
        //Stacks:
        private Stack<string> ExecutedCommands = new Stack<string>();
        private Stack<string> LastExecCommands = new Stack<string>();

        //Constructor:
        public Form1()
        {
            InitializeComponent();
            FormController.form1 = this;
            Hide();
            Config cfg = new Config();
            cfg.ReadConfig();
            Settings.Default.ApplicationName = Settings.Default.ApplicationName.Replace("{version}", $"v{GetVersion}");
            Settings.Default.ServerIP = cfg.GetValue("ServerIP");
            Settings.Default.ServerPort = Convert.ToUInt16(cfg.GetValue("ServerPort"));
            Settings.Default.ServerRconPassword = cfg.GetValue("RCONPassword");
        }

        private string GetVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormController.formLoading.Show();
            Commands.AppendConsole($">> {Settings.Default.ApplicationName} started!");
            Commands.AppendConsole(">> Listing online players");
            Commands.ListPlayersOnline();
            Commands.UpdateForm();
            picLogo.Image = new Bitmap(Resources.ldt_logo);
            ServerEvents.Create();
            txtCommand.Focus();
            txtEnvironment.Text = Commands.GetServerEnvironment();
            txtProtocol.Text = Commands.GetServerProtocol().ToString();
            txtVersion.Text = Commands.GetServerVersion();
            FormController.formLoading.Hide();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (txtCommand.Text != "")
            {
                string Command = txtCommand.Text;
                if (Commands.IsSayCommand(txtCommand.Text)) Command += " | sent via SRX.HeadAdmin.";
                Commands.AppendConsole("Server response: ");
                Commands.AppendConsole(Commands.SendRCON(Command));
                Commands.AppendConsole("");
                txtCommand.Focus();
                ExecutedCommands.Push(txtCommand.Text);
                txtCommand.Text = "";
            }
        }

        //Key pressed event
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonSend.PerformClick();
            if (e.KeyCode == Keys.Escape) { FormExit FE = new FormExit(); FE.ShowDialog(); }
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.R) buttonRefresh.PerformClick();
            if (e.KeyCode == Keys.Up)
            {
                try
                {
                    string Command = ExecutedCommands.Pop();
                    txtCommand.Text = Command;
                    LastExecCommands.Push(Command);
                }
                catch { }
            }
            else if (e.KeyCode == Keys.Down)
            {
                try
                {
                    string Command = LastExecCommands.Pop();
                    txtCommand.Text = Command;
                    ExecutedCommands.Push(Command);
                }
                catch { }
            }
        }

        private void listPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextExecuteCommand.Enabled = false;
            try
            {
                string nickname = listPlayers.GetItemText(listPlayers.SelectedItem);
                if (nickname == "No Players Online!") return;
                string[] userinfo = nickname.Split(' ');
                if (Convert.ToInt32(userinfo[0]) > 9) nickname = nickname.Remove(0, 3);
                else if (Convert.ToInt32(userinfo[0]) < 10) nickname = nickname.Remove(0, 2);
                txtSelectedPlayer.Text = nickname;
                contextExecuteCommand.Enabled = true;
            }
            catch { }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            worker_refresh.RunWorkerAsync();
            return;

            //buttonRefresh.Enabled = false;
            //Commands.ListPlayersOnline();
            //Commands.UpdateForm();
            //buttonRefresh.Enabled = true;
            //SharedClass.ScanMinutes = 0;
            //SharedClass.ScanSeconds = 0;

            //string[] timeleft = txtTimeleft.Text.Split(':');

            //string[] playerInfo = lblPlayers.Text.Split(' ');
            //string[] playerOnline = playerInfo[2].Split('/');

            //string map = txtMap.Text.Contains("_") ? txtMap.Text.Split('_')[1] : txtMap.Text;
                
            //Speech.Speak($"Server Online, current map: {map}.");
            //Speech.Speak($"Players online: {playerOnline[0]} by {playerOnline[1]}.");
            //Speech.Speak($"Timeleft: {timeleft[0]} minutes{(timeleft.Length > 1 ? $" and {timeleft[1]} seconds." : ".")}");
            //Speech.Speak($"Nextmap is: {txtNextmap.Text.Replace('_', ' ')}");
            //Speech.Speak($"Server ping response equals: {txtLag.Text} milliseconds.");
        }

        private void buttonSlay_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                try
                {
                    string nickname = txtSelectedPlayer.Text;
                    string cmd = "amx_slay " + "\"" + nickname + "\"";
                    Commands.SendRCON(cmd);
                    txtSelectedPlayer.Text = "None";
                    Logs.AppendLogs(LogsType.SlayLogs, "Player \"" + nickname + "\" has been slayed.");
                    Commands.AppendConsole(">> Slay command executed on " + nickname);
                }
                catch { Commands.AppendConsole(">> Invalid nickname!"); }
            }
        }

        private void buttonKick_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                try
                {
                    string nickname = txtSelectedPlayer.Text;
                    string cmd = "amx_kick " + "\"" + nickname + "\"";
                    Commands.SendRCON(cmd);
                    txtSelectedPlayer.Text = "None";
                    Logs.AppendLogs(LogsType.KickLogs, "Player \"" + nickname + "\" has been kicked.");
                    Commands.AppendConsole(">> Kick command executed on " + nickname);
                }
                catch { Commands.AppendConsole(">> Invalid nickname!"); }
            }
        }

        private void buttonSlap_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                try
                {
                    FormSlap FS = new FormSlap();
                    FS.ShowDialog();
                    if (Settings.Default.Temp_SlapPower >= 0)
                    {
                        string nickname = txtSelectedPlayer.Text;
                        string cmd = "amx_slap " + "\"" + nickname + "\" +" + Settings.Default.Temp_SlapPower + "";
                        Commands.SendRCON(cmd);
                        txtSelectedPlayer.Text = "None";
                        Logs.AppendLogs(LogsType.SlapLogs, "Player \"" + nickname + "\" has been slapped with " + Settings.Default.Temp_SlapPower + " damage.");
                        Commands.AppendConsole(">> Slap command executed on " + nickname + " doing " + Settings.Default.Temp_SlapPower + " damage!");
                        Settings.Default.Temp_SlapPower = -1;
                    }
                }
                catch { Commands.AppendConsole(">> Invalid nickname!"); }
            }
        }

        #region Timer Tick Events
        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (Settings.Default.Temp_ScanSeconds >= 60)
            {
                Settings.Default.Temp_ScanMinutes++;
                Settings.Default.Temp_ScanSeconds = 0;
            }
            else Settings.Default.Temp_ScanSeconds++;
            if (Settings.Default.Temp_ScanMinutes > 0)
                txtScan.Text = Settings.Default.Temp_ScanMinutes.ToString() + " minutes " + Settings.Default.Temp_ScanSeconds.ToString() + " seconds ago";
            else txtScan.Text = Settings.Default.Temp_ScanSeconds.ToString() + " seconds ago";
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            buttonRefresh.PerformClick();
        }
        #endregion

        private void buttonBan_Click(object sender, EventArgs e)
        {
            //"amx_ban <name or #userid> <minutes> [reason]"
            if (HasPlayerSelected())
            {
                try
                {
                    FormBan FB = new FormBan();
                    FB.ShowDialog();
                    if (!Settings.Default.Temp_AllowBan) { Commands.AppendConsole(">> Ban canceled!"); return; }
                    string nickname = txtSelectedPlayer.Text;
                    string cmd = "";


                    if (Program.banMethod == BanMethod.None)
                    {
                        Commands.AppendConsole("");
                        return;
                    }
                    else if (Program.banMethod == BanMethod.AmxBan)
                    {
                        if (Settings.Default.Temp_BanTime >= 0)
                            cmd = "amx_ban " + "\"" + nickname + "\" +" + Settings.Default.Temp_BanTime + " \"" + Settings.Default.Temp_BanReason + "\"";
                        else cmd = "amx_ban " + "\"" + nickname + "\" +" + 0 + " \"" + Settings.Default.Temp_BanReason + "\"";
                    }
                    else if (Program.banMethod == BanMethod.SSBan)
                    {
                        if (Settings.Default.Temp_BanTime >= 0)
                            cmd = "amx_ssban " + "\"" + nickname + "\" +" + Settings.Default.Temp_BanTime + " \"" + Settings.Default.Temp_BanReason + "\"";
                        else cmd = "amx_ssban " + "\"" + nickname + "\" +" + 0 + " \"" + Settings.Default.Temp_BanReason + "\"";
                    }

                    Commands.SendRCON(cmd);
                    txtSelectedPlayer.Text = "None";
                    Commands.AppendConsole(">> --------------------");
                    Commands.AppendConsole(">> Ban command executed on " + nickname);
                    Commands.AppendConsole(">> Bantime: " + Settings.Default.Temp_BanTime + " minutes");
                    Commands.AppendConsole(">> Reason: " + Settings.Default.Temp_BanReason);
                    Commands.AppendConsole(">> Banned on: " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString());
                    Commands.AppendConsole(">> --------------------");



                    if (Program.banMethod == BanMethod.AmxBan)
                    {
                        Logs.AppendLogs(LogsType.BanLogs, ">> --------------------");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban command executed on: " + nickname + "");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban method: AMX Bans");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban time: " + Settings.Default.Temp_BanTime.ToString() + " minutes");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Reason: " + Settings.Default.Temp_BanReason);
                        Logs.AppendLogs(LogsType.BanLogs, ">> Banned on: " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString());
                    }
                    else if (Program.banMethod == BanMethod.SSBan)
                    {
                        Logs.AppendLogs(LogsType.BanLogs, ">> --------------------");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban command executed on: " + nickname + "");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban method: screenshot bans");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Ban time: " + Settings.Default.Temp_BanTime.ToString() + " minutes");
                        Logs.AppendLogs(LogsType.BanLogs, ">> Reason: " + Settings.Default.Temp_BanReason);
                        Logs.AppendLogs(LogsType.BanLogs, ">> Banned on: " + DateTime.Now.ToString("dd/MM/yyyy") + " " + DateTime.Now.ToLongTimeString());
                    }


                    Settings.Default.Temp_BanTime = 0;
                    Settings.Default.Temp_BanReason = "";
                    Settings.Default.Temp_AllowBan = false;
                }
                catch { Commands.AppendConsole(">> Invalid nickname!"); }
            }
        }

        private void buttonChangeMap_Click(object sender, EventArgs e)
        {
            FormChangeMap FCM = new FormChangeMap();
            FCM.ShowDialog();
            Commands.AppendConsole(Settings.Default.Temp_IsMapChanged ? ">> Map successfully changed!" : ">> Map Selection Canceled!");
        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {
            txtConsole.SelectionStart = txtConsole.Text.Length; //Set the current caret position at the end
            txtConsole.ScrollToCaret(); //Now scroll it automatically
        }

        #region Right click context buttons clicked
        private void toolSlap_Click(object sender, EventArgs e)
        {
            buttonSlap.PerformClick();
        }

        private void toolSlay_Click(object sender, EventArgs e)
        {
            buttonSlay.PerformClick();
        }

        private void toolKick_Click(object sender, EventArgs e)
        {
            buttonKick.PerformClick();
        }

        private void toolBan_Click(object sender, EventArgs e)
        {
            buttonBan.PerformClick();
        }
        #endregion

        private void txtCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                try
                {
                    string Command = ExecutedCommands.Pop();
                    txtCommand.Text = Command;
                    LastExecCommands.Push(Command);
                }
                catch { }
            }
            else if (e.KeyCode == Keys.Down)
            {
                try
                {
                    string Command = LastExecCommands.Pop();
                    txtCommand.Text = Command;
                    ExecutedCommands.Push(Command);
                }
                catch { }
            }
        }

        private void buttonNextMap_Click(object sender, EventArgs e)
        {
            new FormChangeNextMap().ShowDialog();
            Commands.AppendConsole(Settings.Default.Temp_IsNextMapChanged ? ">> Next map updated successfully!" : ">> Next Map Selection Canceled!");
        }

        private void buttonUnban_Click(object sender, EventArgs e)
        {
            new FormUnban().ShowDialog();
        }

        private void toolClear_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
        }

        private void toolCopy_Click(object sender, EventArgs e)
        {
            txtConsole.Copy();
        }

        private void worker_refresh_DoWork(object sender, DoWorkEventArgs e)
        {
            //hostname
            long lag = Commands.PingServer();
            worker_refresh.ReportProgress(Convert.ToInt32(lag));
            this.Invoke(new MethodInvoker(delegate
            {
                buttonRefresh.Enabled = false;
                Commands.ListPlayersOnline();
                Commands.UpdateForm();
                buttonRefresh.Enabled = true;
                Settings.Default.Temp_ScanMinutes = 0;
                Settings.Default.Temp_ScanSeconds = 0;
                string[] timeleft = txtTimeleft.Text.Split(':');
                string[] playerInfo = lblPlayers.Text.Split(' ');
                string[] playerOnline = playerInfo[2].Split('/');
                string map = txtMap.Text.Contains("_") ? txtMap.Text.Split('_')[1] : txtMap.Text;
            }));
        }

        private void worker_refresh_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtLag.Text = e.ProgressPercentage.ToString();
            buttonRefresh.Enabled = false;
        }

        private void worker_refresh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonRefresh.Enabled = true;
        }

        private bool HasPlayerSelected()
        {
            if (txtSelectedPlayer.Text == "None")
            {
                Commands.AppendConsole(">> Select player first!");
                return false;
            }
            return true;
        }
    }
}