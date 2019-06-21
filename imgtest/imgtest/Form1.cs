using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imgtest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Image[] img = { imgtest.Properties.Resources.img1, imgtest.Properties.Resources.img2, imgtest.Properties.Resources.img3, imgtest.Properties.Resources.img4, imgtest.Properties.Resources.img5 };
        int num = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (num <= 4 && num >= 0)
            {
                pictureBox1.Image = img[num];
                num--;
                if (num == 0)
                {
                    num = 4;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "自動播放")
            {
                button3.Text = "停止播放";
                timer1.Start();
            }
            else {
                button3.Text = "自動播放";
                timer1.Stop();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (num <= 4&&num>=0)
            {
                pictureBox1.Image = img[num];
                num++;
                if (num==4) {
                    num = 0;
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img[1];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img[2];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img[3];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = img[4];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((num + 1) <= 4)
            {
                num++;
            }
            else {
                num=0;
            }
            pictureBox1.Image = img[num];
        }
    }
}
