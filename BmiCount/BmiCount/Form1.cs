using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BmiCount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double height, weight, bmi;
            height = double.Parse(textBox1.Text) / 100;
            weight = double.Parse(textBox2.Text);
            bmi = weight / (height * height);
            //textBox3.Text = bmi.ToString("#0.00");
            if (bmi < 18.5)
            {
                textBox3.Text = bmi.ToString("#0.00") + " 體重過輕:p";
            }
            else if (bmi < 24)
            {
                textBox3.Text = bmi.ToString("#0.00") + " 正常範圍:)";
            }
            else if (bmi < 27)
            {
                textBox3.Text = bmi.ToString("#0.00") + " 過重:|";
            }
            else if (bmi < 30)
            {
                textBox3.Text = bmi.ToString("#0.00") + "中度肥胖:O";
            }
            else
            {
                textBox3.Text = bmi.ToString("#0.00") + " 重度肥胖:(";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
