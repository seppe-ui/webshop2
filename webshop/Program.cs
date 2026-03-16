using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product Laptop = new Product(1,"HP ProBook",899.99,5);
            Laptop.ToonInfo();
            Console.WriteLine();

            Product Muis = new Product(2,"Logitech Lightweight",49.99,5);
            Muis.ToonInfo();
            Console.WriteLine();

            Product Toetsenbord = new Product(3,"Steelseries Apex Pro TKL",59.99,5);
            Toetsenbord.ToonInfo();
            Console.WriteLine();


            Console.ReadKey();
            List<Product> Producten = new List<Product>();
            bool Switch = true;

            Console.WriteLine("test");
            Console.WriteLine("1");
            int nummer = int.Parse(Console.ReadLine());

            while (Switch == true) 
            { 
            switch(nummer) 
                {
                    case 1: 
                            Console.Write($"welke product wilt je toevoegen: ");
                            string product = Console.ReadLine();
                            break;
                    case 2:
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
