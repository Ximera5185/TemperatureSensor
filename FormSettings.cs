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
        Device1 form1 = new Device1();

        public FormSettings()
        {
            InitializeComponent();

            trackBarTemperature.Value += form1.CriticalTemperature;

            labelSettingsShowTemperature.Text = trackBarTemperature.Value.ToString();
        }

        private void trackBarTemperature_Scroll(object sender, EventArgs e)
        {
            form1.SetCriticalTemperature(trackBarTemperature.Value);

            labelSettingsShowTemperature.Text = trackBarTemperature.Value.ToString();

            File.WriteAllText("settings.txt", form1.CriticalTemperature.ToString());
        }
    }
}
