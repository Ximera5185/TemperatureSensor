using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureSensor
{
    public partial class Device1 : Form
    {
        public static bool isOpenPort = false;
        public static string PortName = "";
        public Device1()
        {
            TemperatureString = "";

            FileSettingsName = "settings.txt";

            InitializeComponent();

            SetSavedCriticalTemperature();

            СheckСonnectionAsync();
        }

        private static string Package { set; get; }
        private int Temperature { get; set; }
        private string TemperatureString { get; set;}
        private string FileSettingsName { get; set; }

        public static void SetPackage(string package) 
        {
            Package = package;
        }

        public static string GetPackage()
        {
           return Package;
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

                    connect.Connecting(PortName,mySerialPort);
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
            if (File.Exists(FileSettingsName) == false)
            {
                File.WriteAllText(FileSettingsName, CriticalTemperature.ToString());
            }
            else
            {
                string temperature = File.ReadAllText(FileSettingsName);

                CriticalTemperature = Convert.ToInt32(temperature);
            }
        }

        private void MySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string inputData = File.ReadAllText(FileSettingsName);

            label4.Visible = false;

           /* if (isOpenPort)
            {*/
                mySerialPort.Write(inputData);
               /* MessageBox.Show("Отдали запись в фаил");*/
           /* }*/
            /*MessageBox.Show("Не отдали запись ");*/

            int.TryParse(inputData, out int numbers);

            CriticalTemperature = numbers;

            Package = mySerialPort.ReadLine();

            TemperatureString = Package.Substring(4);

            Temperature = Convert.ToInt32(TemperatureString);

            label1.Invoke((MethodInvoker) delegate {label1.Text = TemperatureString;});

            ChangeColor(Temperature, CriticalTemperature);
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
       
        private void SettingsClick(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings();

            formSettings.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             new Form2();
        }
    }
}