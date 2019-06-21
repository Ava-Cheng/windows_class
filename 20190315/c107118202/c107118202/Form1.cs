using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c107118202
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "我要學會C#";
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "我要學會C#!!!!!";
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            label1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("程式設計好好玩");
        }
    }
}
