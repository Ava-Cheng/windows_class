using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace switchTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (textBox1.Text) {
                case "a":
                    //textBox2.Text = "a是母音";
                    //break;
                case "e":
                    //textBox2.Text = "e是母音";
                    //break;
                case "i":
                    //textBox2.Text = "i是母音";
                    //break;
                case "o":
                    //textBox2.Text = "o是母音";
                    //break;
                case "u":
                    //textBox2.Text = "u是母音";
                    textBox2.Text = textBox1.Text+"是母音";
                    break;
                default:
                    textBox2.Text = "是子音";
                    break;

            }
        }
    }
}
