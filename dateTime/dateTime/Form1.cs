using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dateTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Text;
            //西元
            //dateTimePicker1.CustomFormat = "'Today is:' hh:mm:ss dddd MMMM dd, yyyy";

            //民國
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = string.Format("民國{0}/MM/dd", dateTimePicker1.Value.AddYears(-1911).Year.ToString("00"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //現在
            label1.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //明天
            label1.Text = DateTime.Now.AddDays(1).ToString("MM/dd");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime cdate = DateTime.Now;
            label1.Text = "民國年：" + Convert.ToInt16(cdate.AddYears(-1911).Year) + cdate.ToString("/MM/dd");
        }
    }
}
