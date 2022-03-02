//srgjanx

using System;
using System.Collections.Generic;
using System.IO;

namespace SRX.HeadAdmin.Utils
{
    public class Config
    {
        public delegate void ConfigErrorEventHandler(string errorMessage);

        public event ConfigErrorEventHandler OnErrorOccurred;

        private Dictionary<string, string> cfg;

        public void ReadConfig()
        {
            try
            {
                Dictionary<string, string> cfg = new Dictionary<string, string>();
                string[] lines = File.ReadAllLines(Properties.Settings.Default.ConfigFilePath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line) && !line.StartsWith(";"))
                    {
                        string key = "";
                        string val = "";
                        bool readKey = true;
                        foreach (char x in line)
                        {
                            if (x == '=' && readKey)
                            {
                                readKey = false;
                                continue;
                            }
                            if (readKey)
                                key += x;
                            else
                                val += x;
                        }
                        cfg.Add(key, val);
                    }
                }
                this.cfg = cfg;
            }
            catch (Exception ex)
            {
                OnErrorOccurred?.Invoke(ex.Message);
            }
        }

        public string GetValue(string Variable)
        {
            try
            {
                return cfg?[Variable];
            }
            catch(Exception ex)
            {
                OnErrorOccurred?.Invoke(ex.Message);
            }
            return null;
        }

        public bool IsTrue(string Variable)
        {
            string val = GetValue(Variable);
            return !string.IsNullOrEmpty(val) && (val.ToLower() == "true" || val == "1");
        }
    }
}