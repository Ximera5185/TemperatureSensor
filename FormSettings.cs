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
        Form1 form1 = new Form1();

        public FormSettings()
        {
            InitializeComponent();

            trackBarTemperature.Value += form1.criticalTemperature;

            labelSettingsShowTemperature.Text = trackBarTemperature.Value.ToString();
        }

        private void trackBarTemperature_Scroll(object sender, EventArgs e)
        {
            form1.criticalTemperature = trackBarTemperature.Value;

            labelSettingsShowTemperature.Text = trackBarTemperature.Value.ToString();

            File.WriteAllText("settings.txt", form1.criticalTemperature.ToString());
        }
    }
}
