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
                        break
                }
            }
            Console.ReadKey();
        }
    }
}
