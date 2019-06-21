using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IncomeCount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double tax_rate;
            int money = int.Parse(textBox1.Text);
            double money_total = 0;
            switch (true) {
                case true when money <= 500000:
                    tax_rate = 0.05;
                    money_total = money * tax_rate;
                    break;
                case true when money <= 1000000:
                    tax_rate = 0.1;
                    money_total = money * tax_rate-25000;
                    break;
                case true when money <= 2000000:
                    tax_rate = 0.2;
                    money_total = money * tax_rate-125000;
                    break;
                case true when money <= 4000000:
                    tax_rate = 0.3;
                    money_total = money * tax_rate-325000;
                    break;
                default:
                    tax_rate = 0.4;
                    money_total = money * tax_rate-625000;
                    break;
            }
            textBox2.Text = money_total.ToString("#0");
        }
    }
}
