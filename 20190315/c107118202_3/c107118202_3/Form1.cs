using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c107118202_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (Convert.ToUInt32(textBox1.Text) < 60)
            {
                label1.Text = "你的分數為:" + textBox1.Text+ "    再接再厲~不及格";
            }else {
                label1.Text = "你的分數為:" + textBox1.Text + "    恭喜你有及格喔:)"; ;
            }
        }
    }
}
