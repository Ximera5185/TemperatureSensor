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
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int temperature;

        readonly int criticalTemperature = 100;

        string inputDataPort = "";

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            temperature = Convert.ToInt32(mySerialPort.ReadLine());

            inputDataPort = Convert.ToString(temperature);

            label1.Text = inputDataPort;

            ChangeColor(temperature, criticalTemperature);
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

        private void ChangeColor(int temperature, int criticalTemperature)
        {
            if (temperature >= criticalTemperature)
            {
                label1.ForeColor = Color.Red;

                label2.ForeColor = Color.Red;
            }
            else
            {
                label1.ForeColor = Color.FromArgb(58, 204, 41);

                label2.ForeColor = Color.FromArgb(58, 204, 41);
            }
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

                    updatePortList.Enabled = false;

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

                updatePortList.Enabled = true;

                label1.Text = "";

                buttonConnect.Text = "Подключиться";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string comPort = "";
            string key = "term";

            int delay = 1000;
            int portBaudRate = 9600;

            foreach (string port in SerialPort.GetPortNames())
            {
                SerialPort serialPort = new SerialPort(port);

                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();

                    serialPort.BaudRate = portBaudRate;
                }

                Thread.Sleep(delay);

                if (serialPort.BytesToRead > 0)
                {      
                    string package = serialPort.ReadLine();

                    if (package.StartsWith(key))
                    {
                        comPort = port;
                        
                        serialPort.Close();

                        MessageBox.Show("Ключ опознан");

                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"Порт {port} молчит");
 
                    serialPort.Close();
                }
            }          
           

            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    
                    mySerialPort.PortName = comPort;
                    mySerialPort.Open();

                    showBoxPorts.Enabled = false;

                    updatePortList.Enabled = false;

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

                updatePortList.Enabled = true;

                label1.Text = "";

                buttonConnect.Text = "Подключиться";
            }
        }
    }
}
