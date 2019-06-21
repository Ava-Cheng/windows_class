using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace forTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 折舊計算_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double value;
            double start = Convert.ToDouble(textBox1.Text);
            double end = Convert.ToDouble(textBox3.Text);
            int n = Convert.ToInt32(textBox2.Text);
            for (int i = 1; i <= n; i++)
            {
                value = start - (start - end) * i / n;
                textBox4.Text = textBox4.Text + i + "年後價值為" + value + "\r\n";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
