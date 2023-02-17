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
            foreach(var item in list)Console.WriteLine(item.ProductNumber);
            //1. 計算所有商品的總價格
            int sum =list.Select(item => item.Price).Sum();
            Console.WriteLine($"所有商品總價格: {sum:C}");
            //2. 計算所有商品的平均價格
            decimal avg=list.Select(item => Convert.ToDecimal(item.Price)).Average();
            Console.WriteLine($"所有商品的平均價格: {avg:C2}");
            //3. 計算商品的總數量
            int count=list.Count();
            Console.WriteLine($"商品總數量: {count}");
            //4. 計算商品的平均數量
            var avgQuantity=list.Select(item =>item.ProductQuantity).Average();
            Console.WriteLine($"商品的平均數量: {avgQuantity:N}");
            //5. 找出哪一項商品最貴
            var expensive_find = list.Max(x => x.Price) ;
            var expensive = list.FirstOrDefault(x=>x.Price==expensive_find);
            Console.WriteLine($"最貴的商品: {expensive.ProductName}");
            //6. 找出哪一項商品最便宜
            var cheap_find=list.Min(x=> x.Price) ;
            var cheap=list.FirstOrDefault(x=>x.Price==cheap_find);
            Console.WriteLine($"最便宜的商品: {cheap.ProductName}");
            //7. 計算產品類別為 3C 的商品總價
            var price_ALL_3C_find = list.Where (x => x.ProductCategory == "3C").Sum(x=>x.Price);
            //foreach(var item in price_ALL_3C_find) 
            //{ 
            //    Console.WriteLine($"{item.ProductCategory == "3C"}"); 
            //}
            //var price_ALL_3C = price_ALL_3C_find.Sum(x => x.Price);
            Console.WriteLine($"產品類別為3C的商品總價: {price_ALL_3C_find}");

            foreach (var item in list)
            {
                Console.WriteLine($"{item.ProductName}-{item.ProductNumber}-{item.Price}-{item.ProductQuantity}-{item.ProductCategory}");
            }

            Console.ReadLine();
        }


        static List<Product> CreatList()
        {
            FileStream path = new FileStream(@"product.csv", FileMode.Open);
            StreamReader sr = new StreamReader(path);
            string[] all = sr.ReadToEnd().Split(',','\n');
            foreach(var item in all) { Console.WriteLine(item); }
            var list = new List<Product>();

            int n = 5;
            Console.WriteLine(all.Length/5);
            for(int i = 0; i < all.Length/5-1; i++)
            {
                list.Add(new Product() {
                    ProductNumber = all[n],
                    ProductName = all[n + 1],
                    ProductQuantity = int.Parse(all[n + 2]),
                    Price = int.Parse(all[n + 3]),
                    ProductCategory = all[n + 4]
                });
                    
                n += 5;
                
            }
            return list;


            
        }
    }
}
