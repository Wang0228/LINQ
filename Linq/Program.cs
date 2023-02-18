using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var list = CreatList();

            //1. 計算所有商品的總價格
            int sum =list.Select(item => item.Price).Sum();
            Console.WriteLine($"1.所有商品總價格: {sum:C}\n");

            //2. 計算所有商品的平均價格
            decimal avg=list.Select(item => Convert.ToDecimal(item.Price)).Average();
            Console.WriteLine($"2.所有商品的平均價格: {avg:C2}\n");

            //3. 計算商品的總數量
            int count=list.Sum(x=>x.ProductQuantity);
            Console.WriteLine($"3.商品總數量: {count}\n");

            //4. 計算商品的平均數量
            var avgQuantity=list.Select(item =>item.ProductQuantity).Average();
            Console.WriteLine($"4.商品的平均數量: {avgQuantity:N}\n");

            //5. 找出哪一項商品最貴
            var expensive = list.Where(x=>x.Price==list.Max(y=>y.Price));
            foreach(var item in expensive) { Console.WriteLine($"5.最貴的商品: {item.ProductName} , 價格:{item.Price:C}\n"); }

            //6. 找出哪一項商品最便宜
            var cheap=list.Where(x=>x.Price==list.Min(y=>y.Price));
            foreach(var item in cheap) { Console.WriteLine($"6.最便宜的商品: {item.ProductName} , 價格:{item.Price:C}\n"); }

            //7. 計算產品類別為 3C 的商品總價
            var priceALL3C = list.Where (x => x.ProductCategory=="3C").Sum(x=>x.Price);
            Console.WriteLine($"7.產品類別為3C的商品總價: {priceALL3C:C} \n");

            //8. 計算產品類別為飲料及食品的商品價格
            var drinkFoodPrice = list.Where(x => x.ProductCategory == "飲料").Sum(x => x.Price) + list.Where(x => x.ProductCategory == "食品").Sum(x => x.Price);
            Console.WriteLine($"8.商品類別為飲料及食品的商品價格: {drinkFoodPrice:C} \n");

            //9. 找出所有商品類別為食品，而且商品數量大於 100 的商品
            var foodQuantity=list.Where(x=>x.ProductCategory=="食品").Where(x=>x.ProductQuantity>100);
            Console.WriteLine($"9.食品數量大於100的商品: \n");
            foreach(var x in foodQuantity) { Console.WriteLine($"{x.ProductName} , 數量:{x.ProductQuantity}" ); }

            //10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
            var productGroup1000=list.Where(x=>x.Price>1000).GroupBy(x=>x.ProductCategory);
            Console.WriteLine("\n10.各類別商品價格大於1000:\n");
            foreach(var x in productGroup1000)
            {
                Console.WriteLine($"{x.Key}:");
                foreach(var y in x)
                {
                    Console.WriteLine($"{y.ProductName} , 價格:{y.Price}");   
                }
            }

            //11. 呈上題，請計算該類別底下所有商品的平均價格
            double categoryAvg = 0;
            Console.WriteLine("\n11.各類別商品價格大於1000的平均價格:\n");
            foreach (var x in productGroup1000)
            {
                categoryAvg = x.Average(y => y.Price);
                Console.WriteLine($"{x.Key}: {categoryAvg:C}");
            }

            //12. 依照商品價格由高到低排序
            var gpDescending = list.OrderByDescending(x => x.Price);
            Console.WriteLine("\n12.價格由高到低排序:\n");
            foreach (var x in gpDescending) { Console.WriteLine($"{x.ProductNumber}-{x.ProductName}-{x.ProductQuantity}-{x.Price}-{x.ProductCategory}"); }

            //13. 依照商品數量由低到高排序
            var gp = list.OrderBy(x => x.ProductQuantity);
            Console.WriteLine("\n13.數量由低到高排序:\n");
            foreach(var x in gp) { Console.WriteLine($"{x.ProductNumber}-{x.ProductName}-{x.ProductQuantity}-{x.Price}-{x.ProductCategory}"); }

            //14. 找出各商品類別底下，最貴的商品
            var productGroup = list.GroupBy(x => x.ProductCategory);
            Console.WriteLine("\n14.各類別最貴的商品:");
            foreach(var item in productGroup)
            {
                var expensiveCategory=item.Where(x => x.Price==item.Max(y => y.Price));
                Console.WriteLine($"\n{item.Key}:");
                foreach(var z in expensiveCategory) { Console.WriteLine( $"{z.ProductNumber}/{z.ProductName}/{z.ProductQuantity}/{z.Price}/{z.ProductCategory}"); }
            }

            //15. 找出各商品類別底下，最便宜的商品
            Console.WriteLine("\n15.各類別最便宜的商品:");
            foreach(var item in productGroup)
            {
                var cheapCategory = item.Where(x => x.Price == item.Min(y => y.Price));
                Console.WriteLine($"\n{item.Key}");
                foreach (var z in cheapCategory) { Console.WriteLine($"{z.ProductNumber}/{z.ProductName}/{z.ProductQuantity}/{z.Price}/{z.ProductCategory}"); }
            }

            //16. 找出價格小於等於 10000 的商品
            var lower10000 = list.Where(x => x.Price < 10000);
            Console.WriteLine("\n16.價格小於10000商品:\n");
            foreach(var x in lower10000) { Console.WriteLine($"{x.ProductNumber}/{x.ProductName}/{x.ProductQuantity}/{x.Price}/{x.ProductCategory}"); }

            //17. 製作一頁 4 筆總共 5 頁的分頁選擇器
            int SKIP = 0;
            string st = "y";
            do
            {
                
                var list1=list.Skip(SKIP).Take(4);
                Console.WriteLine("----------------------------------------------------\n");
                Console.WriteLine("商品編號/商品名稱/商品數量/商品價格/商品分類");
                foreach (var x in list1)
                {
                    Console.WriteLine($"\n{x.ProductNumber}/{x.ProductName}/{x.ProductQuantity}/{x.Price}/{x.ProductCategory}");
                }
                Console.WriteLine("\n下一頁請按y，上一頁請按n，結束請按其他任意鍵");
                Console.WriteLine("\n----------------------------------------------------");
                st = Console.ReadLine();
                if (st == "y"||st == "Y")
                {
                    SKIP += 4;
                }
                else if(st=="n"||st == "N")
                {
                    SKIP -= 4;
                }
                if(SKIP>16)
                {
                    SKIP = 16;
                }
                else if (SKIP < 0)
                {
                    SKIP = 0;
                }
            } while (st=="y"||st=="Y"||st=="n"||st=="N");


        }


        static List<Product> CreatList()
        {
            var list = new List<Product>();
            string[] all=new string[5];
            string path = @"product.csv";
            var open = new StreamReader(path);
            var read = open.ReadLine();
            while (!open.EndOfStream)
            {
                read=open.ReadLine();
                all = read.Split(',');
                list.Add(new Product()
                {
                    ProductNumber = all[0],
                    ProductName = all[1],
                    ProductQuantity = int.Parse(all[2]),
                    Price = int.Parse(all[3]),
                    ProductCategory = all[4]
                });
            }

            
            return list;


            
        }
    }
}
