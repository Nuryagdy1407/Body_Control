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

namespace Body_Controll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox2.Enabled = true;
            label6.Visible = true;
            label6.Text = "Haýyş edýän enjama baglanyň!";
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            { cmbPorts.Items.Add(port); }
            if (cmbPorts.Items.Count > 0) { cmbPorts.SelectedIndex = 0; }
            AutoPortDetector();
            timer1.Start();
            timer1.Interval = 3000;
        }

        public void AutoPortDetector()
        {
            serialPort1.Close();
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                serialPort1.PortName = port;

            }
            try
            {
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Maglumat beriji enjamlar bilen baglanyşyk kesilen! Baglanyşygy barlaň!");
                Form1_Load(MessageBoxButtons.OKCancel, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hasapla();
        }

        private void hasapla()
        {
            label6.Visible = false;
            int boy = System.Convert.ToInt32(lblBoy.Text);
            int agram = System.Convert.ToInt32(textBox2.Text);
            double weight = Convert.ToDouble(agram);
            double height = Convert.ToDouble(boy);
            double netije = weight/(Math.Pow((height/100),2));

            if (netije >= 25)
            {
                label6.Text = "Siziň boý we agram gatnaşygyňyz kadaly ýagdaýda dal! Size köpüräk sport bilen meşgullanmak maslahat berilýär!!";
                label6.Visible = true;
            }
            else if(netije > 18.5)
            {
                label6.Text = "Siziň boý we agram gatnaşygyňyz kadaly ýagdaýda! Berekella!";
                label6.ForeColor = Color.Green;
                label6.Visible = true;
            }
            else
            {
                label6.Visible = true;
                label6.ForeColor = Color.Red;
                label6.Text = "Siziň boý we agram gatnaşygyňyz kadaly ýagdaýda dal! Size köpüräk iýmek maslahat berilýär!";
            }
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {

            AutoPortDetector();
            if (serialPort1.IsOpen == true)
            {
                btnBaglan.BackColor = Color.Green;
                btnBaglan.Text = "Baglandy";
                btnBaglan.Enabled = false;
                label6.Visible = false;
                textBox2.Enabled = true;
                string boy = serialPort1.ReadLine();
                Console.WriteLine(boy);
                //timer1.Start();
            }
            else
                MessageBox.Show("Enjam baglanmady!");
        }

        private void baglan()
        {
            if (cmbPorts.Items.Count > 0)
            {
                serialPort1 = new System.IO.Ports.SerialPort();
                serialPort1.PortName = cmbPorts.SelectedItem.ToString();
                try
                {
                    serialPort1.Open();
                }
                catch { }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string boy = serialPort1.ReadLine();
            //Console.WriteLine(boy);
            //int boy = Convert.ToInt32(boy);
            lblBoy.Text = boy.ToString();
            if (lblBoy.Text != "" & textBox2.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                label6.Text = "Boýuňyzy we agramyňyzy giriziň!";
                label6.Visible = true;
            }
        }
    }
}
