using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureSensor
{
    internal class Connect
    {
        public Connect() 
        {
            ScanAutomaticPort(ref Form1.portName);
        }
        
        private void ScanAutomaticPort(ref string portName)
        {
           
            string key = "term";

            int delay = 2000;
            int portBaudRate = 9600;

            MessageBox.Show("Начинаем скан портов");

            foreach (string port in SerialPort.GetPortNames())
            {
                SerialPort serialPort = new SerialPort(port);

                if (serialPort.IsOpen == false)
                {
                    serialPort.Open();

                    MessageBox.Show($" {port} открыт {serialPort.IsOpen}");
                    serialPort.BaudRate = portBaudRate;
                }

                Thread.Sleep(delay);

                MessageBox.Show($"Слушаем порт {port}");

                if (serialPort.BytesToRead > 0) // тут косяк
                {
                    Form1.Package = serialPort.ReadLine();

                    if (Form1.Package.StartsWith(key))
                    {
                        serialPort.Close();

                        MessageBox.Show($"Нашли наш порт {port}");

                       // Connect(port, portBaudRate);

                        Form1.isOpenPort = true;

                        portName = port;

                        break;
                    }
                }
                else
                {
                    MessageBox.Show($"Порт {port} молчит");

                    serialPort.Close();
                }
            }
        }
    }
}
