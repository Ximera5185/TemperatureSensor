﻿using System;
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

        bool superPortOpen = false;
        private void ScanAutomaticPort(ref string portName)
        {
            string key = "term";

            int portBaudRate = 9600;
            int dataBytes = 0;

            // MessageBox.Show("Начинаем скан портов");
            while (superPortOpen == false)
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

                    MessageBox.Show($"Слушаем порт {port}");

                    DateTime startTime = DateTime.Now;

                    TimeSpan duration = TimeSpan.FromSeconds(4);

                    MessageBox.Show($"Начинаем считывать данные из буфера порта");

                    while ((DateTime.Now - startTime) < duration) // Слушаем порт 2 сикунды
                    {
                        dataBytes = serialPort.BytesToRead;
                    }

                    if (dataBytes > 0)
                    {
                        MessageBox.Show($"Есть какието данные на порту");


                        Form1.Package = serialPort.ReadLine();

                        MessageBox.Show("Глюк");

                        if (Form1.Package.StartsWith(key)) // тут проблема при повторном подключении
                        {
                            MessageBox.Show("Глюк2");
                            serialPort.Close();

                            MessageBox.Show($"Нашли наш порт {port}");

                            // Connect(port, portBaudRate);

                            // Form1.isOpenPort = true;

                            portName = port;

                            superPortOpen = true;

                            break;
                        }
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