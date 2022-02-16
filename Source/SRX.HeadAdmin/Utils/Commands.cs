//srgjanx

using QueryMaster;
using SRX.HeadAdmin.Properties;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Utils
{
    public class Commands
    {
        public static void AppendConsole(string text)
        {
            FormController.form1.txtConsole.Text += text + "\r\n";
        }

        private static Server GetServerInstance()
        {
            return ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
        }

        #region Server Connection Functions
        private static byte[] PrepareCommand(string command)
        {
            byte[] bufferTemp = Encoding.ASCII.GetBytes(command);
            byte[] bufferSend = new byte[bufferTemp.Length + 4];
            
            //Intial 5 characters as per standard
            bufferSend[0] = 255;
            bufferSend[1] = 255;
            bufferSend[2] = 255;
            bufferSend[3] = 255;
            
            //Copying bytes from challenge rcon to send buffer
            int j = 4;
            for (int i = 0; i < bufferTemp.Length; i++)
            {
                bufferSend[j++] = bufferTemp[i];
            }
            return bufferSend;
        }

        public static string SendRCON(string rcon_cmd)
        {
            UdpClient client = new UdpClient();
            client.Connect(Settings.Default.ServerIP, Settings.Default.ServerPort);

            //Sending challenge command to counter strike server 
            string getChallenge = "challenge rcon\n";
            byte[] bufferSend = PrepareCommand(getChallenge);

            //Send challenge command and get response
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            client.Send(bufferSend, bufferSend.Length);
            byte[] bufferRec = client.Receive(ref remoteIpEndPoint);

            //Retrive number from challenge response 
            string challenge_rcon = Encoding.ASCII.GetString(bufferRec);
            challenge_rcon = string.Join(null, Regex.Split(challenge_rcon, "[^\\d]"));

            //Preparing rcon command to send
            string command = "rcon \"" + challenge_rcon + "\" " + Settings.Default.ServerRconPassword + " " + rcon_cmd + "\n";
            bufferSend = PrepareCommand(command);

            client.Send(bufferSend, bufferSend.Length);
            bufferRec = client.Receive(ref remoteIpEndPoint);
            return Encoding.ASCII.GetString(bufferRec);
        }
        #endregion

        public static bool IsSayCommand(string command)
        {
            return Regex.IsMatch(command, "^say ");
        }

        public static void ListPlayersOnline()
        {
            try
            {
                FormController.form1.listPlayers.Items.Clear();
                Server server = GetServerInstance();
                ReadOnlyCollection<Player> players = server.GetPlayers();
                for (int i = 0; i < players.Count; i++)
                {
                    FormController.form1.listPlayers.Items.Add($"{i+1} {players[i].Name}");
                }
                if (FormController.form1.listPlayers.Items.Count == 0)
                {
                    FormController.form1.listPlayers.ForeColor = Color.Red;
                    FormController.form1.listPlayers.Items.Add("No players online!");
                }
                else
                {
                    FormController.form1.listPlayers.ForeColor = Color.Green;
                }
                server.Dispose();
            }
            catch (Exception)
            {
                AppendConsole("Could not list online players.");
            }
        }

        public static void UpdateForm()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                //Set form text:
                FormController.form1.Text = $"{Settings.Default.ApplicationName} ({serverInfo?.Name ?? "Disconnected"})";
                //Count Online Players
                FormController.form1.lblPlayers.Text = $"Online Players {serverInfo.Players}/{serverInfo.MaxPlayers}";
                //Print IP
                FormController.form1.lblIP.Text = $"{Settings.Default.ServerIP}:{Settings.Default.ServerPort}";
                if (IsServerRunning())
                { 
                    FormController.form1.lblServerStatus.ForeColor = Color.Green; 
                    FormController.form1.lblServerStatus.Text = "Active"; 
                }
                else 
                { 
                    FormController.form1.lblServerStatus.ForeColor = Color.Red; 
                    FormController.form1.lblServerStatus.Text = "Inactive"; 
                }
                long ping = PingServer();
                FormController.form1.txtLag.Text = ping.ToString();
                FormController.form1.txtLag.ForeColor = GetPingColor(ping);
                //
                string currentMap = GetServerMap();
                FormController.form1.txtMap.Text = currentMap;
                new Maps().LoadMapPicutre(currentMap);
                FormController.form1.txtNextmap.Text = GetNextMap();
                FormController.form1.txtTimeleft.Text = GetTimeLeft();
                //
                if (GetServerVACStatus())
                { 
                    FormController.form1.txtVAC.ForeColor = Color.Green; 
                    FormController.form1.txtVAC.Text = "Secured"; 
                }
                else 
                { 
                    FormController.form1.txtVAC.ForeColor = Color.Red; 
                    FormController.form1.txtVAC.Text = "NotSecured"; 
                }
                server.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                FormController.form1.Text = $"{Settings.Default.ApplicationName} (No server found)";
            }
        }

        private static Color GetPingColor(long ping)
        {
            if (ping >= 0 && ping <= 50)
                return Color.Green;
            else if (ping > 50 && ping < 100)
                return Color.Orange;
            else 
                return Color.Red;
        }

        private static bool IsServerRunning()
        {
            try
            {
                bool isRunning = PingServer() >= 0;
                return isRunning;
            }
            catch { }
            return false;
        }

        public static long PingServer()
        {
            try
            {
                Server server = GetServerInstance();
                long ping = server.Ping();
                server.Dispose();
                return ping;
            }
            catch
            {
                AppendConsole("Could not ping server.");
            }
            return 0;
        }

        private static string GetServerMap()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                string map = serverInfo.Map;
                server.Dispose();
                return map;
            }
            catch { }
            return "Unknown";
        }

        private static bool GetServerVACStatus()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                bool isSecure = serverInfo.IsSecure;
                server.Dispose();
                return isSecure;
            }
            catch { }
            return false;
        }

        private static string GetNextMap()
        {
            string output = SendRCON("amx_nextmap");
            string result = string.Empty;
            for (int i = 0; i < output.Count(); i++)
            {
                if (i <= 22) continue;
                else result += output[i];
            }
            return Regex.Replace(result, "\"", string.Empty);
        }

        private static string GetTimeLeft()
        {
            string output = SendRCON("amx_timeleft");
            string result = string.Empty;
            for (int i = 0; i < output.Count(); i++)
            {
                if (i <= 22) continue;
                else result += output[i];
            }
            return Regex.Replace(result, "\"", string.Empty);
        }

        public static string GetServerEnvironment()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                string environment = serverInfo.Environment;
                server.Dispose();
                return environment;
            }
            catch
            {
                AppendConsole("Could not read server environment.");
            }
            return "Unknown";
        }

        public static byte GetServerProtocol()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                byte protocol = serverInfo.Protocol;
                server.Dispose();
                return protocol;
            }
            catch
            {
                AppendConsole("Could not read server protocol.");
            }
            return 0;
        }

        public static string GetServerVersion()
        {
            try
            {
                Server server = GetServerInstance();
                ServerInfo serverInfo = server.GetInfo();
                string version = serverInfo.GameVersion;
                server.Dispose();
            }
            catch
            {
                AppendConsole("Could not read server version.");
            }
            return string.Empty;
        }
    }
}