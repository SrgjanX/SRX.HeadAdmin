//srgjanx

namespace SRX.HeadAdmin.Models
{
    public class MyServerInfo
    {
        public string HostName { get; set; }
        public byte MaxPlayers { get; set; }
        public int Players { get; set; }
        public string CurrentMap { get; set; }
        public string NextMap { get; set; }
        public bool VACStatus { get; set; }
        public string TimeLeft { get; set; }
        public string Environment { get; set; }
        public byte Protocol { get; set; }
        public string Version { get; set; }
    }
}