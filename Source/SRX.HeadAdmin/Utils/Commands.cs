//srgjanx

using QueryMaster;
using SRX.HeadAdmin.Models;
using SRX.HeadAdmin.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace SRX.HeadAdmin.Utils
{
    public class Commands
    {
        public delegate void CommandsActionEventHandler(string message);
        public event CommandsActionEventHandler OnActionDone;

        private Server GetServerInstance => ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);

        #region Server Connection Functions
        private byte[] PrepareCommand(string command)
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

        public string SendRCON(string rcon_cmd)
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

        public bool IsSayCommand(string command)
        {
            return Regex.IsMatch(command ?? string.Empty, "^say ");
        }

        public List<string> GetPlayersOnline()
        {
            List<string> playersOnline = null;
            try
            {
                Server server = GetServerInstance;
                ReadOnlyCollection<Player> players = server.GetPlayers();
                playersOnline = new List<string>();
                for (int i = 0; i < players.Count; i++)
                {
                    playersOnline.Add($"{i + 1} {players[i].Name}");
                }
                server.Dispose();
            }
            catch (Exception)
            {
                OnActionDone?.Invoke("Could not list online players.");
            }
            return playersOnline;
        }

        public bool IsServerRunning => PingServer() >= 0;

        public long PingServer()
        {
            try
            {
                Server server = GetServerInstance;
                long ping = server.Ping();
                server.Dispose();
                return ping;
            }
            catch
            {
                OnActionDone?.Invoke("Could not ping server.");
            }
            return 0;
        }


        public MyServerInfo GetInfo()
        {
            try
            {
                Server server = GetServerInstance;
                ServerInfo serverInfo = server.GetInfo();
                MyServerInfo myServerInfo = new MyServerInfo()
                {
                    HostName = serverInfo?.Name,
                    MaxPlayers = serverInfo?.MaxPlayers ?? 0,
                    Players = serverInfo?.Players ?? 0,
                    CurrentMap = serverInfo?.Map ?? "Unknown",
                    NextMap = GetNextMap,
                    VACStatus = serverInfo?.IsSecure == true,
                    TimeLeft = GetTimeLeft,
                    Environment = serverInfo?.Environment ?? "Unknown",
                    Protocol = serverInfo?.Protocol ?? 0,
                    Version = serverInfo?.GameVersion ?? string.Empty
                };
                server.Dispose();
                return myServerInfo;
            }
            catch { }
            return null;
        }

        private string GetNextMap
        {
            get
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
        }

        private string GetTimeLeft
        {
            get
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
        }

        public void SlapPlayer(string nickName, int slapPower, out string message)
        {
            string cmd = $"amx_slap \"{nickName}\" +{slapPower}";
            SendRCON(cmd);
            Logs.AppendLogs(LogsType.Slap, $"Player \"{nickName}\" has been slapped doing {slapPower} damage.");
            message = $">> Player \"{nickName}\" has been slapped doing {slapPower} damage.";
        }

        public void SlayPlayer(string nickName, out string message)
        {
            string cmd = $"amx_slay \"{nickName}\"";
            SendRCON(cmd);
            Logs.AppendLogs(LogsType.Slay, $"Player \"{nickName}\" has been slayed.");
            message = $">> Player \"{nickName}\" has been slayed.";
        }

        public void KickPlayer(string nickName, out string message)
        {
            string cmd = $"amx_kick \"{nickName}\"";
            SendRCON(cmd);
            Logs.AppendLogs(LogsType.Kick, $"Player \"{nickName}\" has been kicked.");
            message = $">> Player \"{nickName}\" has been kicked!";
        }

        public void BanPlayer(BanMethod banMethod, string nickName, string banReason)
        {
            BanPlayer(banMethod, nickName, 0, banReason);
        }

        public void BanPlayer(BanMethod banMethod, string nickName, int banTime, string banReason)
        {
            string cmd = "";
            if (banMethod == BanMethod.AmxBan)
            {
                cmd = banTime >= 0
                    ? $"amx_ban \"{nickName}\" +{banTime} \"{banReason}\""
                    : "amx_ban " + "\"" + nickName + "\" +" + 0 + " \"" + banReason + "\"";
            }
            else if (banMethod == BanMethod.SSBan)
            {
                cmd = banTime >= 0
                    ? "amx_ssban " + "\"" + nickName + "\" +" + banTime + " \"" + banReason + "\""
                    : "amx_ssban " + "\"" + nickName + "\" +" + 0 + " \"" + banReason + "\"";
            }
            if (!string.IsNullOrEmpty(cmd))
            {
                SendRCON(cmd);
            }
        }

        /// <summary>
        /// Unbans player by given IP address or steam ID.
        /// </summary>
        public void UnbanPlayer(string ip_steamid)
        {
            SendRCON($"amx_unban \"{ip_steamid}\"");
        }

        public void ChangeMap(string map)
        {
            SendRCON($"amx_map {map}");
        }

        public void ChangeNextMap(string nextMap)
        {
            SendRCON($"amx_cvar amx_nextmap {nextMap}");
        }
    }
}