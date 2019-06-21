using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c107118202_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text);
            label2.Text = num.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox3.Text) - Convert.ToInt32(textBox4.Text);
            label8.Text = num.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox5.Text) * Convert.ToInt32(textBox6.Text);
            label9.Text = num.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox7.Text) / Convert.ToInt32(textBox8.Text);
            label10.Text = num.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox9.Text) % Convert.ToInt32(textBox10.Text);
            label11.Text = num.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox11.Text) + Convert.ToInt32(textBox12.Text);
            label7.Text = num.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox11.Text) - Convert.ToInt32(textBox12.Text);
            label7.Text = num.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox11.Text) * Convert.ToInt32(textBox12.Text);
            label7.Text = num.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox11.Text) / Convert.ToInt32(textBox12.Text);
            label7.Text = num.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(textBox11.Text) % Convert.ToInt32(textBox12.Text);
            label7.Text = num.ToString();
        }
    }
}
