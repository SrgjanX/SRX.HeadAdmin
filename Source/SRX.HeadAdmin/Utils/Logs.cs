//srgjanx

using SRX.HeadAdmin.Properties;
using System;
using System.IO;

namespace SRX.HeadAdmin.Utils
{
    public class Logs
    {
        private const string SlapLogs = "Logs\\Slap_Logs.txt";
        private const string SlayLogs = "Logs\\Slay_Logs.txt";
        private const string KickLogs = "Logs\\Kick_Logs.txt";
        private const string BanLogs = "Logs\\Banlist.txt";

        public static void AppendLogs(LogsType e, string Message1)
        {
            string date = DateTime.Now.ToString(Settings.Default.DateFormat);
            string time = DateTime.Now.ToLongTimeString();
            switch(e)
            {
                case LogsType.Slap:
                    File.AppendAllText(SlapLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.Slay:
                    File.AppendAllText(SlayLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.Kick:
                    File.AppendAllText(KickLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.Ban:
                    File.AppendAllText(BanLogs, $"{Message1}\r\n");
                break;
            }
        }
    }
}