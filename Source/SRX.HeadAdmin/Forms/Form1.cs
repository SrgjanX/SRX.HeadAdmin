//srgjanx

using SRX.HeadAdmin.Models;
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
        private ushort scanMinutes = 0;
        private ushort scanSeconds = 0;

        //Stacks:
        private Stack<string> ExecutedCommands = new Stack<string>();
        private Stack<string> LastExecCommands = new Stack<string>();

        //Constructor:
        public Form1()
        {
            InitializeComponent();
            Config cfg = new Config();
            cfg.OnErrorOccurred += Config_OnErrorOccurred;
            cfg.ReadConfig();
            Settings.Default.ApplicationName = Settings.Default.ApplicationName.Replace("{version}", $"v{GetVersion}");
            Settings.Default.ServerIP = cfg.GetValue("ServerIP");
            Settings.Default.ServerPort = Convert.ToUInt16(cfg.GetValue("ServerPort"));
            Settings.Default.ServerRconPassword = cfg.GetValue("RCONPassword");
        }

        private void Config_OnErrorOccurred(string errorMessage)
        {
            AppendConsole(errorMessage);
        }

        private void Maps_OnErrorOccurred(string errorMessage)
        {
            AppendConsole(errorMessage);
        }

        private string GetVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
        }

        private void AppendConsole(string text)
        {
            Invoke(new MethodInvoker(delegate
            {
                txtConsole.Text += text + "\r\n";
            }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Commands commands = new Commands();
            AppendConsole($">> {Settings.Default.ApplicationName} started!");
            ListOnlinePlayers(commands.GetPlayersOnline());
            UpdateForm();
            MyServerInfo myServerInfo = commands.GetInfo();
            if (myServerInfo != null)
            {
                txtEnvironment.Text = myServerInfo.Environment;
                txtProtocol.Text = myServerInfo.Protocol.ToString();
                txtVersion.Text = myServerInfo.Version;
            }
            txtCommand.Focus();
        }

        private void ListOnlinePlayers(List<string> playersOnline)
        {
            AppendConsole(">> Listing online players");
            listPlayers.Items.Clear();
            if (playersOnline == null || playersOnline.Count == 0)
            {
                listPlayers.ForeColor = Color.Red;
                listPlayers.Items.Add("No players online!");
            }
            else
            {
                foreach (string playerOnline in playersOnline)
                {
                    listPlayers.Items.Add(playerOnline);
                }
                listPlayers.ForeColor = Color.Green;
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string command = txtCommand.Text;
            if (command != "")
            {
                Commands commands = new Commands();
                if (commands.IsSayCommand(txtCommand.Text)) 
                    command += Settings.Default.SayCommandSuffix;
                AppendConsole("Server response: ");
                AppendConsole(commands.SendRCON(command));
                AppendConsole("");
                ExecutedCommands.Push(txtCommand.Text);
                txtCommand.Text = "";
                txtCommand.Focus();
            }
        }

        //Key pressed event
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSend.PerformClick();
            }
            if (ModifierKeys == Keys.Control && e.KeyCode == Keys.R)
                buttonRefresh.PerformClick();
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
                if (nickname == "No Players Online!")
                    return;
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
            if(!worker_refresh.IsBusy)
            {
                worker_refresh.RunWorkerAsync();
            }
            return;
        }

        private void buttonSlap_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                try
                {
                    FormSlap FS = new FormSlap();
                    FS.ShouldSlapPlayer += FS_ShouldSlapPlayer;
                    FS.ShowDialog();
                }
                catch { AppendConsole(">> Invalid nickname!"); }
            }
        }

        private void FS_ShouldSlapPlayer(int slapPower)
        {
            new Commands().SlapPlayer(txtSelectedPlayer.Text, slapPower, out string message);
            AppendConsole(message);
        }

        private void buttonSlay_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                string selectedPlayer = txtSelectedPlayer.Text;
                try
                {
                    new Commands().SlayPlayer(selectedPlayer, out string message);
                    AppendConsole(message);
                }
                catch { AppendConsole($">> Could not slay player \"{selectedPlayer}\"!"); }
            }
        }

        private void buttonKick_Click(object sender, EventArgs e)
        {
            if (HasPlayerSelected())
            {
                string selectedPlayer = txtSelectedPlayer.Text;
                try
                {
                    new Commands().KickPlayer(selectedPlayer, out string message);
                    AppendConsole(message);
                }
                catch { AppendConsole($">> Could not kick player \"{selectedPlayer}\"!"); }
            }
        }

        #region Timer Tick Events
        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (scanSeconds >= 60)
            {
                scanMinutes++;
                scanSeconds = 0;
            }
            else 
                scanSeconds++;
            txtScan.Text = scanMinutes > 0
                ? scanMinutes.ToString() + " minutes " + scanSeconds.ToString() + " seconds ago"
                : scanSeconds.ToString() + " seconds ago";
            txtScan.Text = scanMinutes > 0
                ? $"{scanMinutes} minutes, {scanSeconds} seconds ago"
                : $"{scanSeconds} seconds ago";
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
                    FB.ShouldBanPlayer += FB_ShouldBanPlayer;
                    FB.ShowDialog();
                }
                catch { AppendConsole(">> Invalid nickname!"); }
            }
        }

        private void FB_ShouldBanPlayer(BanMethod banMethod, int banTime, string banReason)
        {
            string banMethodName = "unknown";
            switch (banMethod)
            {
                case BanMethod.AmxBan:
                    banMethodName = "AMX Bans";
                    break;
                case BanMethod.SSBan:
                    banMethodName = "screenshot bans";
                    break;
                default:
                    AppendConsole("Unsupported ban method!");
                    return;
            }
            string selectedPlayer = txtSelectedPlayer.Text;
            new Commands().BanPlayer(banMethod, selectedPlayer, banTime, banReason);
            Logs.AppendLogs(LogsType.Ban, ">> --------------------");
            Logs.AppendLogs(LogsType.Ban, $">> Ban command executed on: {selectedPlayer}");
            Logs.AppendLogs(LogsType.Ban, $">> Ban method: {banMethodName}");
            Logs.AppendLogs(LogsType.Ban, $">> Ban time: {banTime} minutes");
            Logs.AppendLogs(LogsType.Ban, $">> Reason: {banReason}");
            Logs.AppendLogs(LogsType.Ban, $">> Banned on: {DateTime.Now.ToString(Settings.Default.DateFormat)} {DateTime.Now.ToLongTimeString()}");
        }

        private void buttonChangeMap_Click(object sender, EventArgs e)
        {
            FormChangeMap FCM = new FormChangeMap();
            FCM.ShouldMapChange += FCM_ShouldMapChange;
            FCM.ShowDialog();
        }

        private void FCM_ShouldMapChange(string map)
        {
            if (!string.IsNullOrEmpty(map))
            {
                new Commands().ChangeMap(map);
                AppendConsole($">> Map successfully changed to {map}!");
            }
        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {
            //Set the current caret position at the end:
            txtConsole.SelectionStart = txtConsole.Text.Length;
            //Now scroll it automatically:
            txtConsole.ScrollToCaret();
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
            FormChangeNextMap formChangeNextMap = new FormChangeNextMap();
            formChangeNextMap.ShouldChangeNextMap += FormChangeNextMap_ShouldChangeNextMap;
            formChangeNextMap.ShowDialog();
        }

        private void FormChangeNextMap_ShouldChangeNextMap(string map)
        {
            if (!string.IsNullOrEmpty(map))
            {
                new Commands().ChangeNextMap(map);
                AppendConsole($">> Next map updated to {map}!");
            }
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
            Commands commands = new Commands();
            commands.OnActionDone += Commands_OnActionDone;
            long ping = commands.PingServer();
            worker_refresh.ReportProgress(Convert.ToInt32(ping));
            this.Invoke(new MethodInvoker(delegate
            {
                buttonRefresh.Enabled = false;
                ListOnlinePlayers(commands.GetPlayersOnline());
                UpdateForm();
                buttonRefresh.Enabled = true;
                scanMinutes = scanSeconds  = 0;
                string[] timeleft = txtTimeleft.Text.Split(':');
                string[] playerInfo = lblPlayers.Text.Split(' ');
                string[] playerOnline = playerInfo[2].Split('/');
                string map = txtMap.Text.Contains("_") ? txtMap.Text.Split('_')[1] : txtMap.Text;
            }));
        }

        private void Commands_OnActionDone(string message)
        {
            AppendConsole(message);
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
                AppendConsole(">> Select player first!");
                return false;
            }
            return true;
        }

        public void UpdateForm()
        {
            try
            {
                Commands commands = new Commands();
                MyServerInfo myServerInfo = commands.GetInfo();
                if (myServerInfo == null)
                    throw new Exception("Could not connect to server!");

                //Set form text:
                Text = $"{Settings.Default.ApplicationName} ({myServerInfo?.HostName ?? "Disconnected"})";
                //Count Online Players
                lblPlayers.Text = $"Online Players {myServerInfo.Players}/{myServerInfo.MaxPlayers}";
                //Print IP
                lblIP.Text = $"{Settings.Default.ServerIP}:{Settings.Default.ServerPort}";
                if (commands.IsServerRunning)
                {
                    lblServerStatus.ForeColor = Color.Green;
                    lblServerStatus.Text = "Active";
                }
                else
                {
                    lblServerStatus.ForeColor = Color.Red;
                    lblServerStatus.Text = "Inactive";
                }
                long ping = commands.PingServer();
                txtLag.Text = ping.ToString();
                txtLag.ForeColor = GetPingColor(ping);
                txtMap.Text = myServerInfo.CurrentMap;
                //load map image:
                if (picMap.Image != null)
                    picMap.Image.Dispose();
                Maps maps = new Maps();
                maps.OnErrorOccurred += Maps_OnErrorOccurred;
                picMap.Image = maps.LoadMapPicutre(myServerInfo.CurrentMap);
                //
                txtNextmap.Text = myServerInfo.NextMap;
                txtTimeleft.Text = myServerInfo.TimeLeft;
                //
                if (myServerInfo.VACStatus)
                {
                    txtVAC.ForeColor = Color.Green;
                    txtVAC.Text = "Secured";
                }
                else
                {
                    txtVAC.ForeColor = Color.Red;
                    txtVAC.Text = "NotSecured";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Text = $"{Settings.Default.ApplicationName} (No server found)";
            }
        }

        private Color GetPingColor(long ping)
        {
            if (ping >= 0 && ping <= 50)
                return Color.Green;
            else if (ping > 50 && ping < 100)
                return Color.Orange;
            else
                return Color.Red;
        }
    }
}