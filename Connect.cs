using System;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TemperatureSensor
{
    internal class Connect
    {
        public Connect()
        {
            ScanAutomaticPort(ref Form1.portName);
        }

        public void Connecting(string portName, int portBaudRate, SerialPort serialPort)
        {
            serialPort.PortName = portName;

            serialPort.Open();
            
            serialPort.BaudRate = portBaudRate;
        }
        private void ScanAutomaticPort(ref string portName)
        {
            SerialPort serialPort;

            string key = "term";

            int portBaudRate = 9600;
            int dataBytes = 0;
            int delayTime = 2;

            bool isPortOpen = false;

            while (isPortOpen == false)
            {
                foreach (string port in SerialPort.GetPortNames())
                {
                    serialPort = new SerialPort(port);

                    if (serialPort.IsOpen == false)
                    {
                        serialPort.Open();

                        serialPort.BaudRate = portBaudRate;
                    }

                    DateTime startTime = DateTime.Now;

                    TimeSpan duration = TimeSpan.FromSeconds(delayTime);

                    while ((DateTime.Now - startTime) < duration)
                    {
                        dataBytes = serialPort.BytesToRead;
                    }

                    if (dataBytes > 0)
                    {
                        serialPort.DiscardInBuffer();

                        Form1.Package = serialPort.ReadLine();

                        if (Form1.Package.StartsWith(key))
                        {
                            serialPort.Close();

                            portName = port;

                            isPortOpen = true;

                            break;
                        }
                    }
                    else
                    {
                        serialPort.Close();
                    }
                }
            }
        }
    }
}