//srgjanx

using SRX.HeadAdmin.Properties;
using SRX.HeadAdmin.Utils;
using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormChangeNextMap : Form
    {
        public FormChangeNextMap()
        {
            InitializeComponent();
        }

        private void buttonChangNextMap_Click(object sender, EventArgs e)
        {
            Commands.SendRCON($"amx_cvar amx_nextmap {comboChooseMap.Text}");
            Settings.Default.Temp_IsNextMapChanged = true;
            Close();
        }

        private void FormChangeNextMap_Load(object sender, EventArgs e)
        {
            Settings.Default.Temp_IsNextMapChanged = false;
            comboChooseMap.Items.Clear();
            Maps maps = new Maps();
            maps.ReadMapsText();
            comboChooseMap.Items.AddRange(maps.MapList.ToArray());
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}