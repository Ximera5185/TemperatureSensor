using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureSensor
{
    public partial class FormSettings : Form
    {
        public FormSettings()
        {
            InitializeComponent();
        }

            Form1 form1 = new Form1();
           
        private void trackBarTemperature_Scroll(object sender, EventArgs e)
        {
           

            form1.criticalTemperature = trackBarTemperature.Value;

            

            labelSettingsShowTemperature.Text = trackBarTemperature.Value.ToString();
        }

      
    }
}
