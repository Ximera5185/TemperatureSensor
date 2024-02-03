using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureSensor
{
    public partial class Form1 : Form
    {
        Connect connect;

        private int _temperature;

        private string _temperatureString = "";

        private string _fileName = "settings.txt";

        public static bool isOpenPort = false;

        public static string  portName = "";

        int portBaudRate = 9600;


        public static string Package { set; get; }
        public Form1()
        {
            InitializeComponent();

            SetSavedCriticalTemperature();


            СheckСonnectionAsync();
        }

        private async Task СheckСonnectionAsync() 
        {
           await Task.Run(() => СheckСonnection());
        }
        private void СheckСonnection() 
        {
            while (true)
            {
                if (mySerialPort.IsOpen == false)
                {
                    label1.Text = "гон";

                    Connect connect = new Connect();

                    Connect(portName,portBaudRate);
                }
            }
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


            int.TryParse(inputData, out int numbers);

            CriticalTemperature = numbers;

            Package = mySerialPort.ReadLine();

            _temperatureString = Package.Substring(4);

            _temperature = Convert.ToInt32(_temperatureString);
    
          
               
                     label1.Invoke((MethodInvoker) delegate
                     {
                         label1.Text = _temperatureString;
                     }
                     );
          
           
            

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
        /* private void ScanAutomaticPort( ref string portName) 
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
                     _package = serialPort.ReadLine();

                     if (_package.StartsWith(key))
                     {
                         serialPort.Close();

                         MessageBox.Show($"Нашли наш порт {port}");

                         Connect(port, portBaudRate);

                         isOpenPort = true;

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
         } */
        public void Connect(string portName, int portBaudRate)
        {
            mySerialPort.PortName = portName;

            mySerialPort.Open();

            mySerialPort.BaudRate = portBaudRate;

           /* autoСonnection.Enabled = true;

            autoСonnection.Text = "Отключиться";*/
        }

      /*  private void AutoСonnectionClick(object sender, EventArgs e)
        {

            string nameOfButton = "Автоподключение";


            if (autoСonnection.Text == nameOfButton)
            {
                Connect(portName, portBaudRate);
            }
            else if (autoСonnection.Text == "Отключиться")
            {
                mySerialPort.Close();

                label1.Text = "";

                autoСonnection.Text = "Автоподключение";
            }
        }*/

        private void settings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();

            formSettings.ShowDialog();
        }

    }
}