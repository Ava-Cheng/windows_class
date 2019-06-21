using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lsosceles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int h = Int32.Parse(textBox1.Text);
            for (int i=1;i<=h;i++) {
                for (int j = 1; j <= i; j++)
                {
                    textBox2.Text = textBox2.Text + "*";
                }
                textBox2.Text = textBox2.Text + "\r\n";
            }
            for (int i = h-1; i >=1; i--)
            {
                for (int j = i; j >= 1; j--)
                {
                    textBox2.Text = textBox2.Text + "*";
                }
                textBox2.Text = textBox2.Text + "\r\n";
            }
        }
    }
}
