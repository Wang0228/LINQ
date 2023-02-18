using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 第二題
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] auto = new int[4];
            string user = "",next="";
            int[] player = { };
            int temp = 0, A = 0, B = 0,j=0;
            bool err = false;

            Console.WriteLine("歡迎來到 1A2B 猜數字的遊戲～");

            do//產生電腦亂數與消除重複
            {
                for (int i = 0; i < 4; i++)
                {
                    j = 0;
                    auto[i] = rnd.Next(0, 4);
                    while (j < i)
                    {
                        if (auto[i] == auto[j])
                        {
                            auto[i] = rnd.Next(0, 4);
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }
                Console.Write("電腦產生:");
                foreach (var i in auto)
                {
                    Console.Write(i);
                }
                do
                {
                    do//使用者輸入格式錯誤, 無輸入、空白、文字、不是四位數
                    {
                        A = 0; B = 0;
                        Console.WriteLine("\n------\n\n請輸入 4 個數字：");
                        user = Console.ReadLine();
                        if (string.IsNullOrEmpty(user) || user.Length != 4 || user.Contains(" ") || !int.TryParse(user, out temp)) 
                        {
                            Console.WriteLine("格式錯誤，請輸入四位數");
                            err = true;
                        }
                        else
                        {
                            err = false;
                        }
                    } while (err);

                    player = user.Select(x => int.Parse(x.ToString())).ToArray();//轉數字陣列

                    for (int i = 0; i < 4; i++)//ElementAt 計算同位置同數字
                    {
                        if (player.ElementAt(i) == auto.ElementAt(i))
                        {
                            A++;
                        }
                    }

                    B = player.Intersect(auto).Count();//Intersect 交集 計算相同數字

                    Console.WriteLine($"判定結果是 {A}A{B - A}B");
                } while (A != 4);
                Console.WriteLine("恭喜你！猜對了！！\n\n------\n你要繼續玩嗎？(y/n):");
                do
                {
                    next = Console.ReadLine();
                    if (next == "n" || next == "N" || next == "y" || next == "Y")
                    {
                        err = false;
                    }
                    else
                    {
                        Console.WriteLine("格式錯誤，請輸入y/n!");
                        err = true;
                    }
                } while (err);
            } while (next == "y");
            Console.WriteLine("遊戲結束，下次再來玩喔～");

        }
    }
}
