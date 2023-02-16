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
            list=list.Skip(1).ToList();
            foreach(var item in list)

            Console.ReadLine();
        }


        static List<Product> CreatList()
        {
            FileStream path = new FileStream(@"product.csv", FileMode.Open);
            StreamReader sr = new StreamReader(path);
            string[] all = sr.ReadToEnd().Split(',','\n');
            foreach(var item in all) { Console.WriteLine(item); }
            var list = new List<Product>();

            int n = 0;
            Console.WriteLine(all.Length/5);
            for(int i = 0; i < all.Length/5; i++)
            {
                list.Add(new Product() {
                    ProductNumber = all[n],
                    ProductName = all[n + 1],
                    ProductQuantity = all[n + 2],
                    Price = all[n + 3],
                    ProductCategory = all[n + 4]
                });
                    
                n += 5;
                
            }
            return list;


            
        }
    }
}
