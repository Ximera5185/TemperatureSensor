using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        private int _temperature;

        private string _temperatureString = "";
        private string _package = "";
        private string _fileName = "settings.txt";

        private bool isOpenPort = false;

        public Form1()
        {
            InitializeComponent();

            SetSavedCriticalTemperature();
        }

        public int CriticalTemperature { get; private set; }

        public void SetCriticalTemperature(int temperature) 
        {
            CriticalTemperature = temperature;
        }
        private void SetSavedCriticalTemperature()
        {
            if (File.Exists(_fileName) == false)
            {
                File.WriteAllText(_fileName, CriticalTemperature.ToString());
            }
            else
            {
                string temperature = File.ReadAllText(_fileName);

                CriticalTemperature = Convert.ToInt32(temperature);
            }
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string inputData = File.ReadAllText(_fileName);

            if (isOpenPort)
            {
                mySerialPort.Write(inputData);
            }

           // CriticalTemperature = Convert.ToInt32(inputData);

            int.TryParse(inputData, out int numbers);

            CriticalTemperature = numbers;

            _package = mySerialPort.ReadLine();

            _temperatureString = _package.Substring(4);

            _temperature = Convert.ToInt32(_temperatureString);

            label1.Text = _temperatureString;

            ChangeColor(_temperature, CriticalTemperature);

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
            string nameOfButton = "Автоподключение";
            string key = "term";

            int delay = 1000;
            int portBaudRate = 9600;

            if (autoСonnection.Text == nameOfButton)
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
                        _package = serialPort.ReadLine();

                        if (_package.StartsWith(key))
                        {
                            serialPort.Close();

                            Connect(port, portBaudRate);

                            isOpenPort = true;

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

                autoСonnection.Text = nameOfButton;
            }
        }

        private void settings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();

            formSettings.ShowDialog();
        }
    }
}