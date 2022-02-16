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

        public static string SendRCON(string rcon_cmd)
        {
            UdpClient client = new UdpClient();
            client.Connect(Settings.Default.ServerIP, Settings.Default.ServerPort);

            //sending challenge command to counter strike server 
            string getChallenge = "challenge rcon\n";
            byte[] bufferSend = PrepareCommand(getChallenge);

            //send challenge command and get response
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            client.Send(bufferSend, bufferSend.Length);
            byte[] bufferRec = client.Receive(ref remoteIpEndPoint);

            //retrive number from challenge response 
            string challenge_rcon = Encoding.ASCII.GetString(bufferRec);
            challenge_rcon = string.Join(null, Regex.Split(challenge_rcon, "[^\\d]"));

            //preparing rcon command to send
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
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ReadOnlyCollection<Player> players = server.GetPlayers();
                short cID = 0;
                foreach (Player i in players)
                {
                    cID++;
                    FormController.form1.listPlayers.Items.Add($"{cID.ToString()} {i.Name}");
                }
                if (FormController.form1.listPlayers.Items.Count == 0)
                {
                    FormController.form1.listPlayers.ForeColor = Color.Red;
                    FormController.form1.listPlayers.Items.Add("No Players Online!");
                }
                else FormController.form1.listPlayers.ForeColor = Color.Green;
                server.Dispose();
            }
            catch (Exception ex)
            {
                Commands.AppendConsole($"Could not list online players.");
            }
        }

        public static void UpdateForm()
        {
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                //Set form text:
                FormController.form1.Text = $"{Settings.Default.ApplicationName} ({si?.Name ?? "Disconnected"})";
                //Count Online Players
                FormController.form1.lblPlayers.Text = $"Online Players {si.Players}/{si.MaxPlayers}";
                //Print IP
                FormController.form1.lblIP.Text = $"{Settings.Default.ServerIP}:{Settings.Default.ServerPort.ToString()}";
                if (Commands.IsServerRunning())
                { FormController.form1.lblServerStatus.ForeColor = System.Drawing.Color.Green; FormController.form1.lblServerStatus.Text = "Active"; }
                else { FormController.form1.lblServerStatus.ForeColor = System.Drawing.Color.Red; FormController.form1.lblServerStatus.Text = "Inactive"; }
                long result = PingServer();
                FormController.form1.txtLag.Text = result.ToString();
                if (result >= 0 && result <= 50) FormController.form1.txtLag.ForeColor = Color.Green;
                else if (result < 0) FormController.form1.txtLag.ForeColor = Color.Red;
                else if (result > 50 && result < 100) FormController.form1.txtLag.ForeColor = Color.Orange;
                if (result >= 100) FormController.form1.txtLag.ForeColor = Color.Red;
                //
                FormController.form1.txtMap.Text = GetServerMap();
                new Maps().LoadMapPicutre(FormController.form1.txtMap.Text);
                FormController.form1.txtNextmap.Text = GetNextMap();
                FormController.form1.txtTimeleft.Text = GetTimeLeft();
                //
                if (GetServerVACStatus())
                { FormController.form1.txtVAC.ForeColor = System.Drawing.Color.Green; FormController.form1.txtVAC.Text = "Secured"; }
                else { FormController.form1.txtVAC.ForeColor = System.Drawing.Color.Red; FormController.form1.txtVAC.Text = "NotSecured"; }
                server.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Occurred", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                FormController.form1.Text = $"{Settings.Default.ApplicationName} (No server found)";
            }
        }

        public static bool IsServerRunning()
        {
            bool result = false;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                if (Commands.PingServer() >= 0)
                    result = true;
                server.Dispose();
            }
            catch
            {
            }
            return result;
        }

        public static long PingServer()
        {
            long result = 0;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                result = server.Ping();
                server.Dispose();
            }
            catch
            {
                Commands.AppendConsole("Could not ping server.");
            }
            return result;
        }

        public static string GetServerMap()
        {
            string result = "Unknown";
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                result = si.Map;
                server.Dispose();
            }
            catch { }
            return result;
        }

        public static bool GetServerVACStatus()
        {
            bool result = false;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                result = si.IsSecure;
                server.Dispose();
            }
            catch { }
            return result;
        }

        public static string GetNextMap()
        {
            string output = Commands.SendRCON("amx_nextmap");
            string result = string.Empty;
            for (int i = 0; i < output.Count(); i++)
            {
                if (i <= 22) continue;
                else result += output[i];
            }
            return Regex.Replace(result, "\"", string.Empty);
        }

        public static string GetTimeLeft()
        {
            string output = Commands.SendRCON("amx_timeleft");
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
            string result = "Unknown";
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                result = si.Environment;
                server.Dispose();
            }
            catch
            {
                Commands.AppendConsole($"Could not read server environment.");
            }
            return result;
        }

        public static byte GetServerProtocol()
        {
            byte result = 0;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                result = si.Protocol;
                server.Dispose();
            }
            catch
            {
                Commands.AppendConsole($"Could not read server protocol.");
            }
            return result;
        }

        public static string GetServerVersion()
        {
            string result = string.Empty;
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                ServerInfo si = server.GetInfo();
                result = si.GameVersion;
                server.Dispose();
            }
            catch
            {
                Commands.AppendConsole($"Could not read server version.");
            }
            return result;
        }

        public static string GetHostname()
        {
            string result = "";
            try
            {
                Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
                result = server?.GetInfo()?.Name;
                server.Dispose();
            }
            catch
            {
                Commands.AppendConsole("Could not get server hostname.");
            }
            return result;
        }
    }
}