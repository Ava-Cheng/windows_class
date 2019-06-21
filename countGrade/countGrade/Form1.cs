using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace countGrade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            int[] score = { 90,90,90,80,80,80};
            for (int i=0;i<6;i++) {
                sum = sum + score[i];
                textBox1.Text += "score["+i+"=]"+score[i]+"\r\n";
                textBox1.Text += "sum=" + sum + "\r\n";
            }
        }
    }
}
