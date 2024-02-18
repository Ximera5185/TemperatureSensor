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
            PortBaudRate = 9600;

            ScanAutomaticPort(ref Device1.PortName);
        }

        private int PortBaudRate { get; set; }

        public void Connecting(string portName, SerialPort serialPort)
        {
            serialPort.PortName = portName;

            serialPort.Open();

            serialPort.BaudRate = PortBaudRate;
        }
        private void ScanAutomaticPort(ref string portName)
        {
            SerialPort serialPort;

            string key = "term";
            
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

                        serialPort.BaudRate = PortBaudRate;
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

                        Device1.SetPackage(serialPort.ReadLine());

                        if (Device1.GetPackage().StartsWith(key))
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