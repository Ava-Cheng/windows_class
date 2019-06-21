using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace postageCount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double weight;
            int money;
            weight = double.Parse(textBox1.Text);
            if (weight <= 5)
            {
                money = 50;
            }
            else if (weight <= 10) {
                money = 70;
            }
            else if (weight <= 15)
            {
                money = 90;
            }
            else if (weight <= 20)
            {
                money = 110;
            }
            else
            {
                money = 200;
            }
            textBox2.Text = money.ToString("#0");
        }
    }
}
