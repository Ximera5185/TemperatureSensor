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

        bool isPortOpen = false;
        private void ScanAutomaticPort(ref string portName)
        {
            string key = "term";

            int portBaudRate = 9600;
            int dataBytes = 0;

            // MessageBox.Show("Начинаем скан портов");
            while (isPortOpen == false)
            {
                foreach (string port in SerialPort.GetPortNames())
                {

                    SerialPort serialPort = new SerialPort(port);

                    if (serialPort.IsOpen == false)
                    {
                        // MessageBox.Show($"{serialPort.IsOpen}");


                        serialPort.Open();


                        // MessageBox.Show($" {port} открыт {serialPort.IsOpen}");

                        serialPort.BaudRate = portBaudRate;


                    }

                   // MessageBox.Show($"Слушаем порт {port}");

                    DateTime startTime = DateTime.Now;

                    TimeSpan duration = TimeSpan.FromSeconds(4);

                   // MessageBox.Show($"Начинаем считывать данные из буфера порта");

                    while ((DateTime.Now - startTime) < duration) // Слушаем порт 2 сикунды
                    {
                        dataBytes = serialPort.BytesToRead;
                    }

                    if (dataBytes > 0)
                    {
                        // MessageBox.Show($"Есть какието данные на порту");

                        serialPort.DiscardInBuffer();

                        Form1.Package = serialPort.ReadLine();

                     // MessageBox.Show("Считали пакет с ключем");

                           // MessageBox.Show($"{Form1.Package}");
                            
                        if (Form1.Package.StartsWith(key)) // тут проблема при повторном подключении
                        {
                            // MessageBox.Show("Передали пакет с ключем");


                            serialPort.Close();

                           // MessageBox.Show($"Нашли наш порт {port}");

                            // Connect(port, portBaudRate);

                            // Form1.isOpenPort = true;

                            portName = port;

                            isPortOpen = true;

                            break;
                        }
                        else { MessageBox.Show("Кривой пакет"); }
                    }
                    else
                    {
                        // MessageBox.Show($"Порт {port} молчит");

                        serialPort.Close();
                    }

                    // System.Threading.Thread.Sleep(100); // Добавить небольшую задержку перед следующей проверкой
                }
            }
        }
    }
}