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

            string comPort = "COM5";

            /*foreach (string port in SerialPort.GetPortNames())
            {
                 SerialPort serialPort = new SerialPort(port);

                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();

                    serialPort.BaudRate = 9600;
                }

                Thread.Sleep(1000);

                if (serialPort.BytesToRead > 0)
                {
                    Console.WriteLine($"Слышу порт {port}");

                    string package = serialPort.ReadLine();

                    if (true)
                    {
                         // mySerialPort.Open();
                       *//* comPort = port;*//*

                        // showBoxPorts.Text = port;

                        //break;
                    }
                }
                else
                {
                    Console.WriteLine($"Порт {port} молчит");

                    serialPort.Close();
                }
            }*/

            // showBoxPorts.Text = "COM5";

            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    // mySerialPort.PortName = showBoxPorts.Text;
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
