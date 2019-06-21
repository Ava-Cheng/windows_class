using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    class Program
    {
        static void Main(string[] args)
        {
            //換幣換算
            /*
            Console.WriteLine("請輸入一個金額:");
            String m = Console.ReadLine();
            int money = int.Parse(m);
            int one = 0;
            int five = 0;
            int ten = 0;
            int fifty = 0;
            int hundred = 0;
            int two_hundred = 0;
            int five_hundred = 0;
            int thousand = 0;
            thousand = money / 1000;
            money = money - thousand * 1000;

            five_hundred = money / 500;
            money = money - five_hundred * 500;

            two_hundred = money / 200;
            money = money - two_hundred * 200;

            hundred = money / 100;
            money = money - hundred * 100;

            fifty = money / 50;
            money = money - fifty * 50;

            ten = money / 10;
            money = money - ten * 10;

            five = money / 5;
            money = money - five * 5;

            one = money / 1;
            money = money - one * 1;

            Console.WriteLine("一千元:"+ thousand+"張");
            Console.WriteLine("五百元:"+ five_hundred + "張");
            Console.WriteLine("兩百元:" + two_hundred + "張");
            Console.WriteLine("一百元:"+ hundred + "張");
            Console.WriteLine("五十元:"+ fifty + "個");
            Console.WriteLine("十元:"+ ten + "個");
            Console.WriteLine("五元:"+ five + "個");
            Console.WriteLine("一元:"+ one + "個");
            */
   
            //format練習
            String name = "胖胖";
            String s = String.Format("嗨{0}你好",name);
            Console.WriteLine(s);
            Console.Read();
        }
    }
}
