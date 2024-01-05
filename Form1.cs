using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    mySerialPort.PortName = showBoxPorts.Text;

                    mySerialPort.Open();

                    showBoxPorts.Enabled = false;

                    buttonConnect.Text = "Отключиться";
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            else if (buttonConnect.Text == "Отключиться")
            {
                mySerialPort.Close();

                showBoxPorts.Enabled = true;

                label1.Text = "";

                buttonConnect.Text = "Подключиться";
            }
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            label1.Text = mySerialPort.ReadLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void UpdatePortListClick(object sender, EventArgs e)
        {
            string [] ports = SerialPort.GetPortNames();

            showBoxPorts.Text = "";

            showBoxPorts.Items.Clear();

            if (ports.Length != 0)
            {
                showBoxPorts.Items.AddRange(ports);

                showBoxPorts.SelectedIndex = 0;
            }
        }
    }
}
