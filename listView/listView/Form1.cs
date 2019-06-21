using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace listView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            setbutton6(false);
        }
        String mode = "";
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            radioButton1.Checked = true;
            enableInput();
            textBox1.Focus();
            setbutton6(true);
            setbutton7(false);
            mode = "add";
        }
        private void enableInput()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
        }
        private void disableInput()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
        }
        private void setbutton6(bool b)
        {
            button6.Visible = b;
            button7.Visible = b;
        }
        private void setbutton7(bool b)
        {
            button1.Visible = b;
            button2.Visible = b;
            button3.Visible = b;
            button4.Visible = b;
            button5.Visible = b;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (mode=="add") {
                ListViewItem Listtext = new ListViewItem(textBox1.Text);
                Listtext.SubItems.Add(textBox2.Text);
                if (radioButton1.Checked) {
                    Listtext.SubItems.Add("男");
                }
                else{
                    Listtext.SubItems.Add("女");
                }
                listView1.Items.Add(Listtext);
                setbutton6(false);
                setbutton7(true);
                disableInput();
            }
            if (mode=="edit") {
                listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;
                if (radioButton1.Checked)
                {
                    listView1.SelectedItems[0].SubItems[2].Text = "男";
                }
                else
                {
                    listView1.SelectedItems[0].SubItems[2].Text = "女";
                }
                setbutton6(false);
                setbutton7(true);
                disableInput();
                listView1.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setbutton6(false);
            setbutton7(true);
            disableInput();
            listView1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count==0) {
                return;
            }
            mode = "edit";
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[0].Text;
            if (listView1.SelectedItems[0].SubItems[2].Text == "男")
            {
                radioButton1.Checked = true;
            }
            else {
                radioButton2.Checked = true;
            }
            enableInput();
            textBox1.Focus();
            setbutton6(true);
            setbutton7(false);
            listView1.Enabled = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            if (listView1.SelectedItems[0].SubItems[2].Text == "男")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("是否刪除","訊息",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes) {
                listView1.SelectedItems[0].Remove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清除", "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                listView1.Items.Clear();
            }
        }
    }
}
