using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace TemperatureSensor
{
    public partial class FormSettings : Form
    {
      

        private int CriticalTemperature { get; set; }
        public FormSettings()
        {
            InitializeComponent();

            TrackBarTemperature.Value += GetCriticalTemperature();

            labelSettingsShowTemperature.Text = TrackBarTemperature.Value.ToString();
        }


        public int GetCriticalTemperature()
        {
            return CriticalTemperature;
        }

        public void SetCriticalTemperature(int criticalTemperature)
        {
            CriticalTemperature = criticalTemperature;
        }

        private void TrackBarTemperature_Scroll(object sender, EventArgs e)
        {
            SetCriticalTemperature(TrackBarTemperature.Value);

            labelSettingsShowTemperature.Text = TrackBarTemperature.Value.ToString();

            File.WriteAllText("settings.txt", GetCriticalTemperature().ToString());
        }
    }
}
