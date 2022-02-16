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
        public List<string> MapList;

        public Maps()
        {
            MapList = new List<string>();
        }

        public void LoadMapPicutre(string map)
        {
            try
            {
                if(FormController.form1.picMap.Image != null)
                    FormController.form1.picMap.Image.Dispose();
                if (!Directory.Exists("Resources\\Maps"))
                    Directory.CreateDirectory("Resources\\Maps");
                string file = $"Resources\\Maps\\{map}.jpg";
                if (File.Exists(file))
                {
                    FormController.form1.picMap.Image = Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Resources\\Maps\\{map}.jpg");
                }
                else
                {
                    string mapURL = GetMapURL(map.ToLower());
                    using (WebClient client = new WebClient())
                    {
                        Uri uri = new Uri(mapURL);
                        client.Headers.Add("User-Agent: Other");
                        client.DownloadFile(uri, $"Resources\\Maps\\{map}.jpg");
                        FormController.form1.picMap.Image = Image.FromFile($"{AppDomain.CurrentDomain.BaseDirectory}Resources\\Maps\\{map}.jpg");
                    }
                }
            }
            catch (Exception ex)
            {
                FormController.form1.picMap.Image = Image.FromFile("Resources\\no_image_available.png");
                Commands.AppendConsole($"Could not load map image, reason: {ex.Message}");
            }
        }

        public void ReadMapsText()
        {
            MapList.Clear();
            string[] lines = File.ReadAllLines(Properties.Settings.Default.MapsFilePath);
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != Properties.Settings.Default.CommentCharacter) MapList.Add(line);
        }

        public void ReadMapsText(ref ComboBox combo)
        {
            MapList.Clear();
            combo.Items.Clear();
            string[] lines = File.ReadAllLines(Properties.Settings.Default.MapsFilePath);
            foreach (string line in lines)
                if (line.Length > 0 && line[0] != Properties.Settings.Default.CommentCharacter) combo.Items.Add(line);
        }

        public string GetMapURL(string map)
        {
            return Settings.Default.MapDownloadURL.Replace("{map}", map);
        }
    }
}