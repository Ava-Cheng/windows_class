using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minN_while
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            int n = 1;
            int total = 0;
            while (total < 1000)
            {
                total += n * n;
                textBox1.Text += Convert.ToString(n) + "*" + Convert.ToString(n) + "\r\n";
                if (total < 1000)
                {
                    textBox1.Text += "+";
                    n++;
                }
            }
            textBox1.Text += "=";
            textBox1.Text += total;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
