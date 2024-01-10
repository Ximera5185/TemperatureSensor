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
using System.IO;
using System.Threading;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            if (File.Exists("settings.txt") == false)
            {
                File.WriteAllText("settings.txt", criticalTemperature.ToString());
            }
            else 
            {
                string temperature = File.ReadAllText("settings.txt");

                criticalTemperature = Convert.ToInt32(temperature);
            }
        }

        public int criticalTemperature = 0;
       
        int temperature;

        string temperatureString = "";
        string package = "";

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            mySerialPort.Write(criticalTemperature.ToString());

            package = mySerialPort.ReadLine();

            temperatureString = package.Substring(4);

            temperature = Convert.ToInt32(temperatureString);

            label1.Text = temperatureString;

            ChangeColor(temperature, criticalTemperature);
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

        private void Connect(string portName, int portBaudRate) 
        {
            mySerialPort.PortName = portName;

            mySerialPort.Open();

            mySerialPort.BaudRate = portBaudRate;

            autoСonnection.Text = "Отключиться";
        }

        private void AutoСonnectionClick(object sender, EventArgs e)
        {
            string key = "term";

            int delay = 1000;
            int portBaudRate = 9600;

            if (autoСonnection.Text == "Автоподключение")
            {
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
                        package = serialPort.ReadLine();

                        if (package.StartsWith(key))
                        {
                            serialPort.Close();

                            Connect(port,portBaudRate);

                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Порт {port} молчит");

                        serialPort.Close();
                    }
                }
            }
            else
            {
                mySerialPort.Close();

                label1.Text = "";

                autoСonnection.Text = "Автоподключение";
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();

            formSettings.ShowDialog();
        }
    }
}
