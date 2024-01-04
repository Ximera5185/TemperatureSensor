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

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (buttonConnect.Text == "Подключиться")
            {
                try
                {
                    string [] ports = SerialPort.GetPortNames();

                    foreach (string port in ports)
                    {
                        MySerialPort.PortName = port;
                        MySerialPort.Open();

                        if (MySerialPort.BytesToRead > 0)
                        {
                            string kay = MySerialPort.ReadLine();

                            if (kay [0] == 'A')
                            {
                                break;
                            }
                            else
                            {
                                MySerialPort.Close();
                            }
                        }
                        else
                        {
                            MySerialPort.Close();
                        }
                    }

                    buttonConnect.Text = "Отключиться";
                }
                catch
                {
                    MessageBox.Show("Ошибка подключения");
                }
            }
            else if (buttonConnect.Text == "Отключиться")
            {
                MySerialPort.Close();
 
                buttonConnect.Text = "Подключиться";
            }
        }
  
        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            /*string temp = MySerialPort.ReadLine();*/

            /*int temperature = Convert.ToInt32(temp);*/

            /*label1.Text = "Температура зоны нагрева " + temp + " Градусов";*/
            /* label1.Invoke((MethodInvoker) (() => label1.Text = "Температура зоны нагрева " + temp + " Градусов"));*/
            label1.Text = MySerialPort.ReadLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
