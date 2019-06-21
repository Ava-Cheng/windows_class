using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace leapYearIF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean yearIF = false;
            int year = int.Parse(textBox1.Text);
            if ((year % 4 == 0) && (year % 400 == 0))
            {
                yearIF = true;
            }
            else if ((year % 100 == 0) && (year % 400 != 0))
            {
                yearIF = false;
            }
            else if (year % 4 == 0)
            {
                yearIF = true;
            }

            if (yearIF)
            {
                textBox2.Text = "是潤年";
            }
            else
            {
                textBox2.Text = "不是潤年";
            }
        }
    }
}
