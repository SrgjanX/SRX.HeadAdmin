//srgjanx

using SRX.HeadAdmin.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Utils
{
    public class Maps
    {
        public delegate void MapsErrorEventHandler(string errorMessage);

        public MapsErrorEventHandler OnErrorOccurred;

        public List<string> MapList;

        public Maps()
        {
            MapList = new List<string>();
        }

        public Image LoadMapPicutre(string map)
        {
            try
            {
                if (!Directory.Exists("Resources\\Maps"))
                    Directory.CreateDirectory("Resources\\Maps");
                string file = $"Resources\\Maps\\{map}.jpg";
                if (File.Exists(file))
                {
                    return Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Resources\\Maps\\{map}.jpg");
                }
                else
                {
                    string mapURL = GetMapURL(map.ToLower());
                    using (WebClient client = new WebClient())
                    {
                        Uri uri = new Uri(mapURL);
                        client.Headers.Add("User-Agent: Other");
                        client.DownloadFile(uri, $"Resources\\Maps\\{map}.jpg");
                        return Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Resources\\Maps\\{map}.jpg");
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke($"Could not load map image, reason: {ex.Message}");
                return Image.FromFile("Resources\\no_image_available.png");
            }
        }

        public void ReadMapsText()
        {
            MapList.Clear();
            string[] lines = File.ReadAllLines(Settings.Default.MapsFilePath);
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != Settings.Default.CommentCharacter) MapList.Add(line);
        }

        public void ReadMapsText(ref ComboBox combo)
        {
            MapList.Clear();
            combo.Items.Clear();
            string[] lines = File.ReadAllLines(Settings.Default.MapsFilePath);
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != Settings.Default.CommentCharacter) combo.Items.Add(line);
        }

        public string GetMapURL(string map)
        {
            return Settings.Default.MapDownloadURL.Replace("{map}", map);
        }
    }
}