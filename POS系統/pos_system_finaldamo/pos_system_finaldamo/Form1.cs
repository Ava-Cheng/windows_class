using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

namespace pos_system_finaldamo
{
    public partial class Form1 : Form
    {
        //全域變數宣告
        int id_num;
        int id_total = 1;
        int ifDiscount = 0;
        String sumMoney;
        double discountNum = 1;
        String serial_number = ((DateTime.Now.ToString("yyyyMMdd")).ToString())+"001";//流水號 格式:當天西元月份日期+三位數流水號
        String numKey = "";//數字鍵
        Boolean payment = false;
        Boolean num = false;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\李偉慈\Desktop\pos_system_finaldamo\pos.mdf;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();

            //外觀
            //杯子尺寸 淡綠色
            fatCup.BackColor = Color.Pink;
            bigCup.BackColor = Color.Pink;
            mediumCup.BackColor = Color.Pink;
            smallCup.BackColor = Color.Pink;
            //冰度 淡藍色
            normalIce.BackColor = Color.PaleTurquoise;
            microIce.BackColor = Color.PaleTurquoise;
            lessIce.BackColor = Color.PaleTurquoise;
            noIce.BackColor = Color.PaleTurquoise;
            //甜度 淡黃色
            normalSweet.BackColor = Color.PaleGoldenrod;
            halfSugar.BackColor = Color.PaleGoldenrod;
            lessSugar.BackColor = Color.PaleGoldenrod;
            sugarFree.BackColor = Color.PaleGoldenrod;
            //新增
            addData.BackColor = Color.Snow;
            //內用外帶外送
            takeout.BackColor = Color.PaleGreen;
            delivery.BackColor = Color.PaleGreen;
            InternalUse.BackColor = Color.PaleGreen;
            //折扣
            discount.BackColor = Color.LightCoral;
            //單筆取消及重新
            singleCancellation.BackColor = Color.LightSkyBlue;
            delete.BackColor = Color.LightSkyBlue;
            //結帳
            checkOut.BackColor = Color.LightSteelBlue;
        }

        //當Form載入
        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化
            ClearData();

            displayOrder();

            //訂單建立時間
            dateTime.Text = System.DateTime.Now.ToString();
            serialNumber.Text = Convert.ToString(serial_number);

            id_total = Convert.ToInt32(display.Rows[display.RowCount - 1].Cells[0].Value);
        }

        private void displayOrder()
        {
            SqlDataAdapter adapter;
            con.Open();// 打開資料庫
            DataTable dt = new DataTable();
            adapter = new SqlDataAdapter("select * from pos", con);//建立查詢

            //建立GridView欄位
            adapter.Fill(dt);
            display.DataSource = dt;
            display.Columns[0].HeaderText = "id";
            display.Columns[1].HeaderText = "餐點名稱";
            display.Columns[2].HeaderText = "甜度";
            display.Columns[3].HeaderText = "冰塊";
            display.Columns[4].HeaderText = "尺寸";
            display.Columns[5].HeaderText = "單價";
            display.Columns[6].HeaderText = "數量";
            display.Columns[7].HeaderText = "小計";
            display.Columns[8].HeaderText = "基礎價格";
            display.Columns[8].Visible = false;
            display.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            con.Close();
        }

        private void InsertDB(string columnName, string keyValue)
        {
            string query;
            int price;
            int subtotal;

            SqlCommand cmd;
            con.Open();

            query = string.Format(@"
                    IF EXISTS (SELECT * FROM pos WHERE id={0})
                        UPDATE pos SET {1} = @keyValue
                        WHERE id={0}
                    ELSE
                        INSERT INTO pos(id, {1}) VALUES({0}, @keyValue)", id_num, columnName);
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@keyValue", keyValue);
            cmd.ExecuteNonQuery();
            con.Close();
            displayOrder();

            if (columnName == "size")//當杯子尺寸被選擇，同時要改變單價
            {
                price = CalSizePrice(id_num, keyValue);
                InsertDB("price", Convert.ToString(price));
            }
            else if (columnName == "num")//當數量改時，同時要改變小計
            {
                subtotal = CalSubtotal(id_num);
                InsertDB("total", Convert.ToString(subtotal));
            }
            else if (columnName == "total")//當小計改變時，同時改變總額 杯數 折扣後金額
            {
                //杯數
                totalNumberOfCups.Text = SumDBValue(id_num, "num").ToString();

                //總額
                sumMoney = SumDBValue(id_num, "total").ToString();
                lumpSum.Text = sumMoney;

                //折扣後金額
                AmountAfterDiscount();
            }
        }

        //計算尺寸價格
        private int CalSizePrice(int id_num, string keyValue)
        {
            int unit_price;//計算單價
            int base_price=1;//基礎價格
            double multiple = 1.0;//倍數

            //依據杯子尺寸給出倍數帶入變數
            if (keyValue == "小杯")
            {
                multiple = 1.0;
            }
            else if (keyValue == "中杯")
            {
                multiple = 1.2;
            }
            else if (keyValue == "大杯")
            {
                multiple = 1.5;
            }
            else if (keyValue == "胖胖杯")
            {
                multiple = 2;
            }

            //查詢基礎價
            base_price = Convert.ToInt32(SearchDB(id_num, "base_price"));

            //計算最後單價
            unit_price = (int)Math.Round(base_price * multiple, 0);

            return unit_price;
        }


        //小計
        private int CalSubtotal(int id_num)
        {
            int price;
            int num;
            int subtotal;

            //查詢基礎價格
            price = Convert.ToInt32(SearchDB(id_num, "price"));
            //查詢杯數
            num = Convert.ToInt32(SearchDB(id_num, "num"));

            //計算小計
            subtotal = price * num;

            return subtotal;
        }

        //從DB撈單筆資料品項
        private String SearchDB(int id_num, String colName)
        {
            String sql_command;
            String sql_result = "";

            SqlCommand cmd;
            con.Open();
            sql_command = string.Format(@"SELECT {0} FROM pos WHERE id={1}", colName, id_num);
            cmd = new SqlCommand(sql_command, con);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
            reader.Read();
            try
            {
                sql_result = reader[colName].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            con.Close();

            return sql_result;
        }

        //總額總杯數加總
        private int SumDBValue(int id_num,String colName)
        {
            int sum = 0;
            String sum_sql;

            SqlCommand cmd;
            con.Open();
            sum_sql = string.Format(@"SELECT {0} FROM pos", colName);
            cmd = new SqlCommand(sum_sql, con);
            SqlDataReader reader_sum = cmd.ExecuteReader();
            while (reader_sum.Read())
            {
                sum += reader_sum.GetInt32(0);
            }
            con.Close();

            return sum;
        }

        //折扣後金額
        private void AmountAfterDiscount() {
            try
            {
                total.Text = Convert.ToInt32(int.Parse(lumpSum.Text) * discountNum).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                total.Text = "";
            }
        }

        //品項和價格寫入DB
            private void pictureBox1_Click(object sender, EventArgs e)
        {
            InsertDB("name", "水蜜桃果汁");
            InsertDB("base_price", "40");
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            InsertDB("name", "西瓜果汁");
            InsertDB("base_price", "25");
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            InsertDB("name", "芒果果汁");
            InsertDB("base_price", "55");
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            InsertDB("name", "番茄果汁");
            InsertDB("base_price", "35");
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            InsertDB("name", "香蕉果汁");
            InsertDB("base_price", "30");
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            InsertDB("name", "百香果果汁");
            InsertDB("base_price", "30");
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            InsertDB("name", "柳橙果汁");
            InsertDB("base_price", "35");
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            InsertDB("name", "檸檬果汁");
            InsertDB("base_price", "20");
        }
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            InsertDB("name", "芋頭奶茶");
            InsertDB("base_price", "40");
        }
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            InsertDB("name", "抹茶鮮奶茶");
            InsertDB("base_price", "65");
        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            InsertDB("name", "珍珠奶茶");
            InsertDB("base_price", "55");
        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            InsertDB("name", "焦糖奶茶");
            InsertDB("base_price", "35");
        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {
            InsertDB("name", "抹香奶茶");
            InsertDB("base_price", "30");
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            InsertDB("name", "鐵觀音奶茶");
            InsertDB("base_price", "35");
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            InsertDB("name", "布丁奶茶");
            InsertDB("base_price", "40");
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            InsertDB("name", "仙草凍奶茶");
            InsertDB("base_price", "65");
        }
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            InsertDB("name", "黑咖啡");
            InsertDB("base_price", "40");
        }
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            InsertDB("name", "白布雷拿鐵");
            InsertDB("base_price", "65");
        }
        private void pictureBox23_Click(object sender, EventArgs e)
        {
            InsertDB("name", "卡布奇諾");
            InsertDB("base_price", "55");
        }
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            InsertDB("name", "奶霜咖啡");
            InsertDB("base_price", "35");
        }
        private void pictureBox24_Click(object sender, EventArgs e)
        {
            InsertDB("name", "厚重拿鐵");
            InsertDB("base_price", "30");
        }
        private void pictureBox22_Click(object sender, EventArgs e)
        {
            InsertDB("name", "焦糖瑪奇朵");
            InsertDB("base_price", "35");
        }
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            InsertDB("name", "綠茶拿鐵");
            InsertDB("base_price", "40");
        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            InsertDB("name", "法式焦糖拿鐵");
            InsertDB("base_price", "65");
        }
        private void pictureBox28_Click(object sender, EventArgs e)
        {
            InsertDB("name", "百花檸檬青");
            InsertDB("base_price", "40");
        }
        private void pictureBox32_Click(object sender, EventArgs e)
        {
            InsertDB("name", "茉綠鮮綠茶");
            InsertDB("base_price", "25");
        }
        private void pictureBox29_Click(object sender, EventArgs e)
        {
            InsertDB("name", "四季春茶");
            InsertDB("base_price", "35");
        }
        private void pictureBox25_Click(object sender, EventArgs e)
        {
            InsertDB("name", "菊花普洱茶");
            InsertDB("base_price", "35");
        }
        private void pictureBox27_Click(object sender, EventArgs e)
        {
            InsertDB("name", "炭焙烏龍茶");
            InsertDB("base_price", "30");
        }
        private void pictureBox31_Click(object sender, EventArgs e)
        {
            InsertDB("name", "錫蘭紅茶");
            InsertDB("base_price", "35");
        }
        private void pictureBox30_Click(object sender, EventArgs e)
        {
            InsertDB("name", "文山清茶");
            InsertDB("base_price", "30");
        }
        private void pictureBox26_Click(object sender, EventArgs e)
        {
            InsertDB("name", "翡翠檸檬茶");
            InsertDB("base_price", "45");
        }
        private void pictureBox35_Click(object sender, EventArgs e)
        {
            InsertDB("name", "抹茶綿綿冰");
            InsertDB("base_price", "75");
            InsertDB("sweetness", "無");
            InsertDB("ice", "無");
        }
        private void pictureBox36_Click(object sender, EventArgs e)
        {
            InsertDB("name", "藍莓綿綿冰");
            InsertDB("base_price", "70");
            InsertDB("sweetness", "無");
            InsertDB("ice", "無");
        }
        private void pictureBox38_Click(object sender, EventArgs e)
        {
            InsertDB("name", "芒果綿綿冰");
            InsertDB("base_price", "85");
            InsertDB("sweetness", "無");
            InsertDB("ice", "無");
        }

        //+1數量-1數量
        private void minusOne_Click(object sender, EventArgs e)
        {
            if (num)
            {
                //System.Diagnostics.Debug.WriteLine("Convert.ToInt32(numKey)");
                //System.Diagnostics.Debug.WriteLine(Convert.ToInt32(numKey));
                numKey = (Convert.ToInt32(SearchDB(id_num, "num")) - 1).ToString();
                InsertDB("num", numKey);
            }
        }
        private void addOne_Click(object sender, EventArgs e)
        {
            if (num)
            {
                numKey = (Convert.ToInt32(SearchDB(id_num, "num")) + 1).ToString();
                InsertDB("num", numKey);
            }
        }

        //數字鍵按鈕區塊
        private void zero_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "0";
                paymentAmount.Text = numKey;
            } else if (num) {
                numKey += "0";
                InsertDB("num", numKey);
            }
        }
        private void one_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "1";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "1";
                InsertDB("num", numKey);
            }
        }
        private void two_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "2";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "2";
                InsertDB("num", numKey);
            }
        }
        private void three_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "3";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "3";
                InsertDB("num", numKey);
            }
        }
        private void four_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "4";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "4";
                InsertDB("num", numKey);
            }
        }
        private void fives_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "5";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "5";
                InsertDB("num", numKey);
            }
        }
        private void six_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "6";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "6";
                InsertDB("num", numKey);
            }
        }
        private void seven_Click(object sender, EventArgs e)
        {
            if (payment) {
                numKey += "7";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "7";
                InsertDB("num", numKey);
            }
        }
        private void eight_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "8";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "8";
                InsertDB("num", numKey);
            }
        }
        private void nine_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                numKey += "9";
                paymentAmount.Text = numKey;
            }
            else if (num)
            {
                numKey += "9";
                InsertDB("num", numKey);
            }
        }
        private void ck_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                paymentAmount.Text = numKey;
                payment = false;
                num = false;
            }
            else if (num)
            {
                payment = false;
                num = false;
            }
        }
        private void clear_Click(object sender, EventArgs e)
        {
            if (payment)
            {
                paymentAmount.Text = "";
            }
            else if (num)
            {
                numKey = "";
                InsertDB("num", numKey);
            }
        }

        //甜度按鈕區塊寫入DB
        private void normalSweet_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇正常甜。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("sweetness", "正常甜");
            }
        }
        private void halfSugar_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇半糖。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("sweetness", "半糖");
            }
        }
        private void lessSugar_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇少糖。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("sweetness", "少糖");
            }
        }
        private void sugarFree_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇無糖。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("sweetness", "無糖");
            }
        }

        //冰塊程度按鈕區塊寫入DB
        private void normalIce_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇正常冰。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("ice", "正常冰");
            }
        }

        private void microIce_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num,"name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇微冰。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {InsertDB("ice", "微冰");
            }
        }
        private void lessIce_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇少冰。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("ice", "少冰");
            }
        }
        private void noIce_Click(object sender, EventArgs e)
        {
            if (SearchDB(id_num, "name").Contains("綿綿冰"))
            {
                MessageBox.Show("您點的品項為冰品，無法選擇去冰。", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                InsertDB("ice", "去冰");
            }
            
        }


        //杯子尺寸按鈕區塊寫入DB
        private void fatCup_Click(object sender, EventArgs e)
        {
            InsertDB("size", "胖胖杯");
        }
        private void bigCup_Click(object sender, EventArgs e)
        {
            InsertDB("size", "大杯");
        }
        private void mediumCup_Click(object sender, EventArgs e)
        {
            InsertDB("size", "中杯");
        }
        private void middleCenter_Click(object sender, EventArgs e)
        {
            InsertDB("size", "小杯");
        }

        //新增資料
        private void addData_Click(object sender, EventArgs e)
        {
            id_total++;
            id_num = id_total;
        }

        //顯示區域點擊事件
        private void display_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.display.Columns[e.ColumnIndex].Name=="num") {
                num = true;
                payment = false;
                numKey = "";
            }

            try
            {
                id_num = Convert.ToInt32(display.Rows[e.RowIndex].Cells[0].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //外帶外送內用
        private void takeout_Click(object sender, EventArgs e)
        {
            label51.Text = "外帶";
        }
        private void delivery_Click(object sender, EventArgs e)
        {
            label51.Text = "外送";
        }
        private void InternalUse_Click(object sender, EventArgs e)
        {
            label51.Text = "內用";
        }

        //折扣
        private void discount_Click(object sender, EventArgs e)
        {
            discountNum = 0.8;
            ifDiscount++;
            //折扣後金額
            if ((ifDiscount % 2) != 0)
            {
                AmountAfterDiscount();
            }
            else
            {
                lumpSum.Text = sumMoney;
            }
            
        }
        //單筆取消
        private void singleCancellation_Click(object sender, EventArgs e)
        {
            string del_row_sql;
            int price = 0;
            int cupNum = 0;

            try {
                price = Convert.ToInt32(SearchDB(id_num, "price"));
                cupNum = Convert.ToInt32(SearchDB(id_num, "num"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //總額 總杯數 折扣後總額 重新計算
            lumpSum.Text = (int.Parse(lumpSum.Text)- price* cupNum).ToString();
            totalNumberOfCups.Text = (int.Parse(totalNumberOfCups.Text) - cupNum).ToString();
            if ((ifDiscount % 2) != 0)
            {
                total.Text = (int.Parse(lumpSum.Text) - price * cupNum).ToString();
            }
            else
            {
                AmountAfterDiscount();
            }
            

            //DataGridViewRow資料列清除
            DataGridViewRow r1 = this.display.CurrentRow;
            this.display.Rows.Remove(r1);
            
            //單筆資料從DB清除資料
            SqlCommand cmd;
            con.Open();
            del_row_sql = string.Format(@"Delete From pos Where id={0}", id_num);
            cmd = new SqlCommand(del_row_sql, con);
            cmd.ExecuteNonQuery();
            con.Close();

            displayOrder();
        }
        //重新
        private void delete_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        //付款額輸入
        private void paymentAmount_Click(object sender, EventArgs e)
        {
            numKey = "";
            num = false;
            payment = true;
        }

        //結帳
        private void checkOut_Click(object sender, EventArgs e)
        {
            int giveChange_num = 0;

            try{
                giveChange_num = int.Parse(paymentAmount.Text) - int.Parse(total.Text);
                giveChange.Text = (giveChange_num).ToString();

                if (giveChange_num < 0)
                {
                    MessageBox.Show("本次消費:$" + total.Text + "共支付:$" + paymentAmount.Text + "您還缺:$" + (int.Parse(total.Text) - int.Parse(paymentAmount.Text)).ToString(), "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else if (label51.Text=="") {
                    MessageBox.Show("尚未選擇用餐方式，請選擇外帶/外送/內用其一", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("本次消費:$" + total.Text + "共支付:$" + paymentAmount.Text + "找零:$" + giveChange_num.ToString(), "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //清除資料
                    ClearData();

                    //流水號+1
                    serialNumber.Text = (Convert.ToDouble(serial_number) + 1).ToString();//流水號+1

                    id_num = 0;
                    id_total = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("請先點餐再進行結帳", "溫馨提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //清空資料
        private void ClearData() {
            string del_sql;

            //清除DB資料
            SqlCommand cmd;
            con.Open();
            del_sql = string.Format(@"TRUNCATE TABLE pos");
            cmd = new SqlCommand(del_sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            displayOrder();

            //清除欄位資料
            paymentAmount.Text = "";
            giveChange.Text = "";
            lumpSum.Text = "";
            totalNumberOfCups.Text = "";
            total.Text = "";

            //訂單成立時間重新取得
            dateTime.Text = System.DateTime.Now.ToString();

            //外帶內用外送lable清除
            label51.Text = "";
        }
    }
}
