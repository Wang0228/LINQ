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
            Console.WriteLine($"所有商品總價格: {sum:C}\n");

            //2. 計算所有商品的平均價格
            decimal avg=list.Select(item => Convert.ToDecimal(item.Price)).Average();
            Console.WriteLine($"所有商品的平均價格: {avg:C2}\n");

            //3. 計算商品的總數量
            int count=list.Sum(x=>x.ProductQuantity);
            Console.WriteLine($"商品總數量: {count}\n");

            //4. 計算商品的平均數量
            var avgQuantity=list.Select(item =>item.ProductQuantity).Average();
            Console.WriteLine($"商品的平均數量: {avgQuantity:N}\n");

            //5. 找出哪一項商品最貴
            var expensive = list.Where(x=>x.Price==list.Max(y=>y.Price));
            foreach(var item in expensive) { Console.WriteLine($"最貴的商品: {item.ProductName}\n"); }

            //6. 找出哪一項商品最便宜
            var cheap=list.Where(x=>x.Price==list.Min(y=>y.Price));
            foreach(var item in cheap) { Console.WriteLine($"最便宜的商品: {item.ProductName} \n"); }

            //7. 計算產品類別為 3C 的商品總價
            var priceALL3C = list.Where (x => x.ProductCategory=="3C").Sum(x=>x.Price);
            Console.WriteLine($"產品類別為3C的商品總價: {priceALL3C} \n");

            //8. 計算產品類別為飲料及食品的商品價格
            var drinkFoodPrice = list.Where(x => x.ProductCategory == "飲料").Sum(x => x.Price) + list.Where(x => x.ProductCategory == "食品").Sum(x => x.Price);
            Console.WriteLine($"商品類別為飲料及食品的商品價格: {drinkFoodPrice} \n");

            //9. 找出所有商品類別為食品，而且商品數量大於 100 的商品
            var foodQuantity=list.Where(x=>x.ProductCategory=="食品").Where(x=>x.ProductQuantity>100);
            Console.WriteLine($"食品數量大於100的商品: ");
            foreach(var x in foodQuantity) { Console.WriteLine(x.ProductName ); }

            //10. 找出各個商品類別底下有哪些商品的價格是大於 1000 的商品
            var productGroup=list.Where(x=>x.Price>1000).GroupBy(x=>x.ProductCategory);
            Console.WriteLine("\n各類別商品價格大於1000:");
            foreach(var x in productGroup)
            {
                Console.WriteLine($"{x.Key}:");
                foreach(var y in x)
                {
                    Console.WriteLine(y.ProductName);   
                }
            }

            //11. 呈上題，請計算該類別底下所有商品的平均價格
            double categoryAvg = 0;
            Console.WriteLine("各類別商品價格大於1000的平均價格:");
            foreach (var x in productGroup)
            {
                categoryAvg = x.Average(y => y.Price);
                Console.WriteLine($"{x.Key}: {categoryAvg}");
            }



            //foreach (var item in list)
            //{
            //    Console.WriteLine($"{item.ProductName}-{item.ProductNumber}-{item.Price}-{item.ProductQuantity}-{item.ProductCategory}");
            //}

            Console.ReadLine();
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
