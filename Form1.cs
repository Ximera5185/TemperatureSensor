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

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    MySerialPort.PortName = comboBoxPorts.Text;
                    MySerialPort.Open();
                    comboBoxPorts.Enabled = false;
                    buttonUpdatePorts.Enabled = false;
                    buttonConnect.Text = "Отключиться";
                }
                catch 
                {

                    MessageBox.Show("Ошибка подключения");
                }
            }
            else if (buttonConnect.Text == "Отключиться")
            {
                MySerialPort.Close();
                comboBoxPorts.Enabled = true;
                buttonUpdatePorts.Enabled = true;
                label1.Text = "";
                buttonConnect.Text = "Подключиться";
            }
        }

        private void buttonUpdatePorts_Click(object sender, EventArgs e)
        {
            string [] ports = SerialPort.GetPortNames();

            comboBoxPorts.Text = "";
            comboBoxPorts.Items.Clear();

            if (ports.Length != 0)
            {
                comboBoxPorts.Items.AddRange(ports);
                comboBoxPorts.SelectedIndex = 0;
            }
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string temp = MySerialPort.ReadLine();

            label1.Text = "Температура зоны нагрева " + temp + " Градусов";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
