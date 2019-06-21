using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Image[] img = { Laba.Properties.Resources.num1, Laba.Properties.Resources.num2, Laba.Properties.Resources.num3, Laba.Properties.Resources.num4, Laba.Properties.Resources.num5, Laba.Properties.Resources.num6, Laba.Properties.Resources.num7 };
        private void button1_Click(object sender, EventArgs e)
        {
            int[] x = new int[3];
            int num = 0;
            Random rand = new Random();
            for (int i=0;i<3;i++) {
                x[i] = rand.Next(1, 8);
                if (i == 0)
                {
                    if (x[i]==1) {
                        pictureBox1.Image = Properties.Resources.num1;
                    }
                    if (x[i] == 2)
                    {
                        pictureBox1.Image = Properties.Resources.num2;
                    }
                    if (x[i] == 3)
                    {
                        pictureBox1.Image = Properties.Resources.num3;
                    }
                    if (x[i] == 4)
                    {
                        pictureBox1.Image = Properties.Resources.num4;
                    }
                    if (x[i] == 5)
                    {
                        pictureBox1.Image = Properties.Resources.num5;
                    }
                    if (x[i] == 6)
                    {
                        pictureBox1.Image = Properties.Resources.num6;
                    }
                    if (x[i] == 7)
                    {
                        pictureBox1.Image = Properties.Resources.num7;
                    }
                }
                if (i == 1)
                {
                    if (x[i] == 1)
                    {
                        pictureBox2.Image = Properties.Resources.num1;
                    }
                    if (x[i] == 2)
                    {
                        pictureBox2.Image = Properties.Resources.num2;
                    }
                    if (x[i] == 3)
                    {
                        pictureBox2.Image = Properties.Resources.num3;
                    }
                    if (x[i] == 4)
                    {
                        pictureBox2.Image = Properties.Resources.num4;
                    }
                    if (x[i] == 5)
                    {
                        pictureBox2.Image = Properties.Resources.num5;
                    }
                    if (x[i] == 6)
                    {
                        pictureBox2.Image = Properties.Resources.num6;
                    }
                    if (x[i] == 7)
                    {
                        pictureBox2.Image = Properties.Resources.num7;
                    }
                }
                if (i == 2)
                {
                    if (x[i] == 1)
                    {
                        pictureBox3.Image = Properties.Resources.num1;
                    }
                    if (x[i] == 2)
                    {
                        pictureBox3.Image = Properties.Resources.num2;
                    }
                    if (x[i] == 3)
                    {
                        pictureBox3.Image = Properties.Resources.num3;
                    }
                    if (x[i] == 4)
                    {
                        pictureBox3.Image = Properties.Resources.num4;
                    }
                    if (x[i] == 5)
                    {
                        pictureBox3.Image = Properties.Resources.num5;
                    }
                    if (x[i] == 6)
                    {
                        pictureBox3.Image = Properties.Resources.num6;
                    }
                    if (x[i] == 7)
                    {
                        pictureBox3.Image = Properties.Resources.num7;
                    }
                }
            }
            
        }
    }
}
