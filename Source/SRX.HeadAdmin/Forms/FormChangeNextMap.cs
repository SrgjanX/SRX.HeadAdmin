//srgjanx

using SRX.HeadAdmin.Utils;
using System;
using System.Windows.Forms;

namespace SRX.HeadAdmin.Forms
{
    public partial class FormChangeNextMap : Form
    {
        public delegate void MapChangeEventHandler(string map);
        public event MapChangeEventHandler ShouldChangeNextMap;

        public FormChangeNextMap()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonChangNextMap_Click(object sender, EventArgs e)
        {
            ShouldChangeNextMap?.Invoke(comboChooseMap.Text);
            Close();
        }

        private void FormChangeNextMap_Load(object sender, EventArgs e)
        {
            comboChooseMap.Items.Clear();
            Maps maps = new Maps();
            maps.ReadMapsText();
            comboChooseMap.Items.AddRange(maps.MapList.ToArray());
            if (comboChooseMap.Items.Count > 0)
                comboChooseMap.SelectedIndex = 0;
        }
    }
}