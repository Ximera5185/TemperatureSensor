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
       
        private void AutoСonnectionClick(object sender, EventArgs e)
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
        }
    }
}
