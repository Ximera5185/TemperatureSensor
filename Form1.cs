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

        public static string portName = "";

        readonly int portBaudRate = 9600;

        public static string Package { set; get; }
        public Form1()
        {
            InitializeComponent();

            SetSavedCriticalTemperature();

            СheckСonnectionAsync();
        }

        private async Task СheckСonnectionAsync()
        {
            await Task.Run(async () => СheckСonnection());
        }

        private void СheckСonnection()
        {
            while (true)
            {
                while (mySerialPort.IsOpen == false)
                {
                    label1.Text = "";

                    label4.Visible = true;

                    Connect connect = new Connect();

                    connect.Connecting(portName,portBaudRate,mySerialPort);
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

            label4.Visible = false;

            if (isOpenPort)
            {

                mySerialPort.Write(inputData);
            }

            int.TryParse(inputData, out int numbers);

            CriticalTemperature = numbers;

            Package = mySerialPort.ReadLine();

            _temperatureString = Package.Substring(4);

            _temperature = Convert.ToInt32(_temperatureString);

            label1.Invoke((MethodInvoker) delegate {label1.Text = _temperatureString;});

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
       
        private void settings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();

            formSettings.ShowDialog();
        }
    }
}