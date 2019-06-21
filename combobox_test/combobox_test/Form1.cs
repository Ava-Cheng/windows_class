using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace combobox_test
{
    public partial class Form1 : Form
    {
        String[] s = { "0-20", "21-40", "41-60", "61-80", "81-" };
        public Form1()
        {
            InitializeComponent();
            for (int i=0;i<5;i++) {
                comboBox2.Items.Add(s[i]);
            }
            comboBox2.SelectedIndex = 2;

            for (int i = 0; i < 5; i++)
            {
                listBox2.Items.Add(s[i]);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox2.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox3.Items.Add(comboBox2.SelectedItem.ToString()); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox3.Items.Add(listBox1.SelectedItem.ToString());
        }
    }
}
