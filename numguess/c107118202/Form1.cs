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
        int[] qustion = new int[4];
        int[] ans = new int[4];

        public static void display2D(int[] q, TextBox display)
        {
            for (int i = 0; i < q.Length; i++)
            {
                display.Text += q[i];
            }
            display.Text += "\r\n";
        }

        public String displayS(int[] q)
        {
            String s = "";
            for (int i = 0; i < q.Length; i++)
            {
                s += q[i];
            }
            return s;
        }

        public static void randomNum(int num, int[] a)
        {
            int[] n = new int[num];
            for (int i = 0; i < n.Length; i++)
            {
                n[i] = i;
            }
            Random rnd = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                int r = rnd.Next(0, n.Length - i);
                a[i] = n[r];
                chooseNum(n, r, i);

            }



        }
        public static void chooseNum(int[] num, int r, int index)
        {
            for (int i = r; i < num.Length - index - 1; i++)
            {
                num[i] = num[i + 1];

            }
        }
        public Form1()
        {
            InitializeComponent();
            randomNum(10, qustion);
            display_1.Text = "請輸入不重複的4個數字\r\n\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int num_4 = 0;
            if (input.Text == "")
            {
                MessageBox.Show("請輸入不重複的4個\"數字\"");
                return;
            }
            else if (num_4 < 0 && num_4 > 1_000)
            {
                MessageBox.Show("請輸入不重複的\"4個\"數字");
                return;
            }

            num_4 = Convert.ToInt16(input.Text);

            for (int i = 0; i < ans.Length; i++)
            {
                ans[i] = num_4 / Convert.ToInt16(Math.Pow(10, 3 - i));

                num_4 -= (Convert.ToInt16(ans[i] * Math.Pow(10, 3 - i)));

            }
            for (int i = 0; i < ans.Length; i++)
            {
                for (int j = 0; j < ans.Length; j++)
                {
                    if (ans[i] == ans[j] && i != j)
                    {
                        MessageBox.Show("請輸入\"不重複\"的4個數字");
                        return;
                    }
                }
            }

            int A = 0;
            int B = 0;
            for (int i = 0; i < qustion.Length; i++)
            {

                for (int j = 0; j < ans.Length; j++)
                {
                    if (qustion[i] == ans[j])
                    {
                        if (i == j)
                        {
                            A++;
                        }
                        else
                        {
                            B++;
                        }

                    }
                }
            }

            String s = String.Format("{0}A{1}B", A, B);

            display_1.Text += displayS(ans) + "\t" + s + "\r\n";
            display2.Text = (s);

            if (A == 4)
            {
                MessageBox.Show("恭喜答對");
                display_1.Text = "請輸入不重複的4個數字";
                input.Text = "";
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("再加油啊啊啊!\r\n答案:" + displayS(qustion));
            display_1.Text = "";
            input.Text = "";
            randomNum(10, qustion);
        }

 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

 

        private void display_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void input_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
