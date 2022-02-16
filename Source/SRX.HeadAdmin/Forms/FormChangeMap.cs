//srgjanx

using SRX.HeadAdmin.Properties;
using SRX.HeadAdmin.Utils;
using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormChangeMap : Form
    {
        public FormChangeMap()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonChangeMap_Click(object sender, EventArgs e)
        {
            Commands.SendRCON($"amx_map {comboChooseMap.Text}");
            Settings.Default.Temp_IsMapChanged = true;
            Close();
        }

        private void FormChangeMap_Load(object sender, EventArgs e)
        {
            Settings.Default.Temp_IsMapChanged = false;
            comboChooseMap.Items.Clear();
            Maps maps = new Maps();
            maps.ReadMapsText();
            comboChooseMap.Items.AddRange(maps.MapList.ToArray());
        }
    }
}