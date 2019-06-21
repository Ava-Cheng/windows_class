using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C107118202
{
    public partial class Form1 : Form
    {
        String[] kind = { "拉麵", "蓋飯", "茶泡飯", "咖哩飯", "小菜" };
        String[,] meals = new string[5, 6]  { { "清湯拉麵  $50", "醬油拉麵  $80", "豚骨拉麵  $100", "札幌味噌拉麵  $110", "八版獅子頭拉麵  $130", "中華鮮蝦魂吞拉麵  $120" }, 
            { "炙燒肉蓋飯  $90", "煎炙烤鮭魚蓋飯  $110", "獅子頭飯  $130", "焗烤鮭魚鬆蓋飯  $130", "藍帶豬排蓋飯  $110", "豬排蓋飯  $90" }, 
            { "燒肉茶泡飯  $50", "鮭魚茶泡飯  $80", "味噌汁燒肉茶泡飯  $80", "牛蒡天婦羅茶泡飯  $90", "海帶芽茶泡飯  $50", "味噌汁牛蒡天婦羅茶泡飯  $100" }, 
            { "原味咖哩飯  $80", "雞肉咖哩飯  $100", "咖哩藍帶豬排  $90", "咖哩鮮蝦可樂餅  $110", "咖哩魚楊天  $110", "咖哩蘑菇野菜  $100" }, 
            { "溏心蛋  $25", "燙青菜  $30", "可樂餅  $75", "藍帶豬排  $70", "可樂  $25", "台灣啤酒  $40" }};
        int[,] money = new int[5, 6] { { 50, 80, 100, 110, 130, 120 }, 
            { 90, 110, 130, 130, 110, 90}, 
            { 50, 80, 80, 90, 50, 100 }, 
            { 80, 100, 90, 110, 110, 100}, 
            { 25, 30, 75, 70, 25, 40 } };
        int total = 0;
        public Form1()
        {
            InitializeComponent();
            listBox1.Text = "";
            listBox2.Text = "";
            listBox3.Text = "";
            for (int i = 0; i < kind.Length; i++)
            {
                comboBox1.Items.Add(kind[i]);
            }
            comboBox1.SelectedIndex = 0;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.SelectedItem.ToString());
            total += (money[comboBox1.SelectedIndex, listBox1.SelectedIndex])*(int)numericUpDown1.Value;
            textBox1.Text = Convert.ToString(total);
            listBox3.Items.Add(Convert.ToString(numericUpDown1.Value));
            numericUpDown1.Value = 1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            for (int j = 0; j <6; j++) {
                listBox1.Items.Add( meals[comboBox1.SelectedIndex, j]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected = listBox2.SelectedItem.ToString();
            listBox3.SelectedIndex = listBox2.SelectedIndex;
            for (int i = 0; i < kind.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (meals[i,j].Equals(selected)) {
                        total -= money[i, j] * Convert.ToInt32(listBox3.SelectedItem.ToString());
                        textBox1.Text = Convert.ToString(total);
                    }
                }
            }
            listBox3.Items.RemoveAt(listBox2.SelectedIndex);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
        }
    }
}
