//srgjanx

//using QueryMaster;

namespace SRX.HeadAdmin.Utils
{
    public class ServerEvents
    {
        public static void Create()
        {
            //Commands.AppendConsole(">> Events are disabled!");
            //Server server = ServerQuery.GetServerInstance(EngineType.Source, Settings.Default.ServerIP, Settings.Default.ServerPort);
            //QueryMaster.Logs logs = server.GetLogs(Settings.Default.ServerPort);
            //logs.Listen(LogListener);
            //logs.PlayerKilled += new EventHandler<KillEventArgs>(logs_PlayerKilled);
            //logs.PlayerConnected += new EventHandler<ConnectEventArgs>(logs_PlayerConnected);
            //logs.PlayerAcquiredWeapon += new EventHandler<WeaponEventArgs>(logs_PlayerAcquiredWeapon);
            //logs.Say += new EventHandler<ChatEventArgs>(logs_Say);
            //Commands.AppendConsole(">> Events Created!");
        }

        //private static void LogListener(string logMsg)
        //{
        //    Commands.AppendConsole($"Log message: {logMsg}");
        //}

        //private static void logs_PlayerAcquiredWeapon(object sender, WeaponEventArgs e)
        //{
        //    Commands.AppendConsole("Player acquired a weapon.");
        //}

        //private static void logs_PlayerKilled(object sender, KillEventArgs e)
        //{
        //    Commands.AppendConsole("Played was killed.");
        //}

        //private static void logs_PlayerConnected(object sender, ConnectEventArgs e)
        //{
        //    Commands.AppendConsole("Played has connected to the server.");
        //}

        //private static void logs_Say(object sender, ChatEventArgs e)
        //{
        //    Commands.AppendConsole($"{e.Player}: {e.Message} @ {e.Timestamp}");
        //}
    }
}