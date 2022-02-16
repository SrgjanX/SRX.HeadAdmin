//srgjanx

using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace xRCON
{
    public class Maps
    {
        public static List<string> ChangeMaps = new List<string>();

        public static void LoadMapPicutre(string Map)
        {
            Map = Map.ToLower();
            string location = GetMapURL(Map);
            SharedClass.f1.picMap.Load(location);
        }

        public static void ReadMapsText()
        {
            ChangeMaps.Clear();
            string[] lines = System.IO.File.ReadAllLines(@"Maps.txt");
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != ';') ChangeMaps.Add(line);
        }

        public static void ReadMapsText(ref ComboBox combo)
        {
            ChangeMaps.Clear();
            combo.Items.Clear();
            string[] lines = System.IO.File.ReadAllLines(@"Maps.txt");
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != ';') combo.Items.Add(line);
        }

        public static string GetMapURL(string Map)
        {
            string returnpoint = "";
            SQLiteConnection DBC_MAPS = new SQLiteConnection("Data Source=maps.db;Version=3;");
            SQLiteCommand CMD_IS_CORRECT_MAP = new SQLiteCommand("SELECT COUNT(1) FROM maps WHERE MapName='" + Map + "';", DBC_MAPS);
            SQLiteCommand CMD_GET_SERVER_MAP = new SQLiteCommand("select Location from maps where MapName='" + Map + "';", DBC_MAPS);
            SQLiteCommand CMD_GET_SERVER_DEF_MAP = new SQLiteCommand("select Location from maps where MapName='Unknown';", DBC_MAPS);
            bool CorrectMap = false;
            SQLiteDataReader sqlr = null;
            DBC_MAPS.Open();
            try
            {
                sqlr = CMD_IS_CORRECT_MAP.ExecuteReader();
                while (sqlr.Read()) CorrectMap = true;
                sqlr.Dispose();
                if (CorrectMap) sqlr = CMD_GET_SERVER_MAP.ExecuteReader();
                else sqlr = CMD_GET_SERVER_DEF_MAP.ExecuteReader();
                sqlr.Read();
                returnpoint = sqlr["Location"].ToString();
                sqlr.Dispose();
            }
            catch { }
            sqlr.Close();
            DBC_MAPS.Close();
            return returnpoint;
        }
    }
}