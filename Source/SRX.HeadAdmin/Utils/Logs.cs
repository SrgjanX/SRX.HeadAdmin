//srgjanx

using System;
using System.IO;

namespace SRX.HeadAdmin.Utils
{
    public class Logs
    {
        private const string MapLogs = "Logs\\Map_Logs.txt";
        private const string SlapLogs = "Logs\\Slap_Logs.txt";
        private const string SlayLogs = "Logs\\Slay_Logs.txt";
        private const string KickLogs = "Logs\\Kick_Logs.txt";
        private const string BanLogs = "Logs\\Banlist.txt";

        public static void AppendLogs(LogsType e, string Message1)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string time = DateTime.Now.ToLongTimeString();
            switch(e)
            {
                case LogsType.MapLogs:
                    File.AppendAllText(MapLogs, $"{Message1} @ {date} {time}\r\n");
                break;
                case LogsType.SlapLogs:
                    File.AppendAllText(SlapLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.SlayLogs:
                    File.AppendAllText(SlayLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.KickLogs:
                    File.AppendAllText(KickLogs, $"{Message1} @ {date} {time}\r\n");
                    break;
                case LogsType.BanLogs:
                    File.AppendAllText(BanLogs, $"{Message1}\r\n");
                break;
            }
        }
    }
}