//srgjanx

using QueryMaster;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using xRCON.Properties;

namespace xRCON
{
    public class Commands
    {
        public static void CreateEvents()
        {
            //v2.0
            //Server server = ServerQuery.GetServerInstance(EngineType.Source, Commands.IP, Commands.Port);
            //QueryMaster.Logs logs = server.GetLogs(Commands.Port);
            //logs.Listen(LogListener);
            //logs.PlayerKilled += new EventHandler<KillEventArgs>(logs_PlayerKilled);
            //logs.PlayerConnected += new EventHandler<ConnectEventArgs>(logs_PlayerConnected);
            //logs.PlayerAcquiredWeapon += new EventHandler<WeaponEventArgs>(logs_PlayerAcquiredWeapon);
            //logs.Say += new EventHandler<ChatEventArgs>(logs_Say);
            //AppendConsole(">> Events Created!");
        }

        public static void AppendConsole(string Text)
        {
            SharedClass.f1.txtConsole.Text += Text + "\r\n";
        }

        #region Server Connection Functions
        public static byte[] PrepareCommand(string command)
        {
            byte[] bufferTemp = Encoding.ASCII.GetBytes(command);
            byte[] bufferSend = new byte[bufferTemp.Length + 4];

            //intial 5 characters as per standard
            bufferSend[0] = byte.Parse("255");
            bufferSend[1] = byte.Parse("255");
            bufferSend[2] = byte.Parse("255");
            bufferSend[3] = byte.Parse("255");

            //copying bytes from challenge rcon to send buffer
            int j = 4;

            for (int i = 0; i < bufferTemp.Length; i++)
            {
                bufferSend[j++] = bufferTemp[i];
            }
            return bufferSend;
        }

        public static string SendRCON(string RCON_CMD)
        {
            UdpClient client = new UdpClient();
            client.Connect(SharedClass.IP, SharedClass.Port);

            //sending challenge command to counter strike server 
            string getChallenge = "challenge rcon\n";
            byte[] bufferSend = PrepareCommand(getChallenge);

            //send challenge command and get response
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            client.Send(bufferSend, bufferSend.Length);
            byte[] bufferRec = client.Receive(ref RemoteIpEndPoint);

            //retrive number from challenge response 
            string challenge_rcon = Encoding.ASCII.GetString(bufferRec);
            challenge_rcon = string.Join(null, Regex.Split(challenge_rcon, "[^\\d]"));

            //preparing rcon command to send
            string command = "rcon \"" + challenge_rcon + "\" " + SharedClass.RCONPW + " " + RCON_CMD + "\n";
            bufferSend = PrepareCommand(command);

            client.Send(bufferSend, bufferSend.Length);
            bufferRec = client.Receive(ref RemoteIpEndPoint);
            return Encoding.ASCII.GetString(bufferRec);
        }
        #endregion

        public static bool IsSayCommand(string Command)
        {
            return Regex.IsMatch(Command, "^say ");
        }

        public static void ListPlayersOnline()
        {
            try
            {
                SharedClass.f1.listPlayers.Items.Clear();
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ReadOnlyCollection<Player> players = server.GetPlayers();
                short cID = 0;
                foreach (Player i in players)
                {
                    cID++;
                    SharedClass.f1.listPlayers.Items.Add($"{cID.ToString()} {i.Name}");
                }
                if (SharedClass.f1.listPlayers.Items.Count == 0)
                {
                    SharedClass.f1.listPlayers.ForeColor = System.Drawing.Color.Red;
                    SharedClass.f1.listPlayers.Items.Add("No Players Online!");
                }
                else SharedClass.f1.listPlayers.ForeColor = System.Drawing.Color.Green;
                server.Dispose();
            }
            catch { }
        }

        private static void LogListener(string logMsg)
        {
            Commands.AppendConsole($"Log Message: {logMsg}");
        }

        private static void logs_PlayerAcquiredWeapon(object sender, WeaponEventArgs e)
        {
            Commands.AppendConsole("WEAPON ACQUIRED EVENT");
        }

        private static void logs_PlayerKilled(object sender, KillEventArgs e)
        {
            Commands.AppendConsole("KILL EVENT");
        }

        private static void logs_PlayerConnected(object sender, ConnectEventArgs e)
        {
            Commands.AppendConsole("CONNECT EVENT");
        }

        private static void logs_Say(object sender, ChatEventArgs e)
        {
            Commands.AppendConsole($"{e.Player}: {e.Message} @ {e.Timestamp}");
        }

        public static void UpdateForm()
        {
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                //Count Online Players
                SharedClass.f1.lblPlayers.Text = $"Online Players {si.Players}/{si.MaxPlayers}";
                //Print IP
                SharedClass.f1.lblIP.Text = $"{SharedClass.IP}{Settings.Default.ServerIPPortSeparator}{SharedClass.Port.ToString()}";
                if (Commands.IsServerRunning())
                { SharedClass.f1.lblServerStatus.ForeColor = System.Drawing.Color.Green; SharedClass.f1.lblServerStatus.Text = "Active"; }
                else { SharedClass.f1.lblServerStatus.ForeColor = System.Drawing.Color.Red; SharedClass.f1.lblServerStatus.Text = "Inactive"; }
                long result = PingServer();
                SharedClass.f1.txtLag.Text = result.ToString();
                if (result >= 0 && result <= 50) SharedClass.f1.txtLag.ForeColor = Color.Green;
                else if (result < 0) SharedClass.f1.txtLag.ForeColor = Color.Red;
                else if (result > 50 && result < 100) SharedClass.f1.txtLag.ForeColor = Color.Orange;
                if (result >= 100) SharedClass.f1.txtLag.ForeColor = Color.Red;
                //
                SharedClass.f1.txtMap.Text = GetServerMap();
                Maps.LoadMapPicutre(SharedClass.f1.txtMap.Text);
                SharedClass.f1.txtNextmap.Text = GetNextMap();
                SharedClass.f1.txtTimeleft.Text = GetTimeLeft();
                //
                if (GetServerVACStatus())
                { SharedClass.f1.txtVAC.ForeColor = System.Drawing.Color.Green; SharedClass.f1.txtVAC.Text = "Secured"; }
                else { SharedClass.f1.txtVAC.ForeColor = System.Drawing.Color.Red; SharedClass.f1.txtVAC.Text = "NotSecured"; }
                server.Dispose();
            }
            catch { }
        }

        public static bool IsServerRunning()
        {
            bool Result = false;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                if (Commands.PingServer() >= 0)
                    Result = true;
                server.Dispose();
            }
            catch { }
            return Result;
        }

        public static long PingServer()
        {
            long returnpoint = 0;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                returnpoint = server.Ping();
                server.Dispose();
            }
            catch { }
            return returnpoint;
        }

        public static string GetServerMap()
        {
            string returnpoint = "Unknown";
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                returnpoint = si.Map;
                bool xx = si.IsSecure;
                server.Dispose();
            }
            catch { }
            return returnpoint;
        }

        public static bool GetServerVACStatus()
        {
            bool returnpoint = false;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                returnpoint = si.IsSecure;
                server.Dispose();
            }
            catch { }
            return returnpoint;
        }

        public static string GetNextMap()
        {
            string output = Commands.SendRCON("amx_nextmap");
            string returnpoint = string.Empty;
            for (int i = 0; i < output.Count(); i++)
            {
                if (i <= 22) continue;
                else returnpoint += output[i];
            }
            returnpoint = Regex.Replace(returnpoint, "\"", string.Empty);
            return returnpoint;
        }

        public static string GetTimeLeft()
        {
            string output = Commands.SendRCON("amx_timeleft");
            string returnpoint = string.Empty;
            for (int i = 0; i < output.Count(); i++)
            {
                if (i <= 22) continue;
                else returnpoint += output[i];
            }
            returnpoint = Regex.Replace(returnpoint, "\"", string.Empty);
            return returnpoint;
        }

        public static string GetServerEnvironment()
        {
            string returnpoint = "Unknown";
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                returnpoint = si.Environment;
                server.Dispose();
            }
            catch { }
            return returnpoint;
        }

        public static byte GetServerProtocol()
        {
            byte returnpoint = 0;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                returnpoint = si.Protocol;
                server.Dispose();
            }
            catch { }
            return returnpoint;
        }

        public static string GetServerVersion()
        {
            string Result = string.Empty;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, SharedClass.IP, SharedClass.Port);
                ServerInfo si = server.GetInfo();
                Result = si.GameVersion;
                server.Dispose();
            }
            catch { }
            return Result;
        }
    }
}