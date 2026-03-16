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



            int i = 0;


            while (Switch == true) 
            {
                Console.WriteLine($" kies 1 of 2");
                int nummer = int.Parse(Console.ReadLine());
                switch (nummer) 
                {
                    case 1:

                        Console.Write("welke product wilt je toevoegen: ");
                        string product = Console.ReadLine();

                        Console.WriteLine("prijs van de product:");
                        int prijs = int.Parse(Console.ReadLine());

                        Console.WriteLine("de voorraad van de product:");
                        int voorraad = int.Parse(Console.ReadLine());

                        Product nieuwProduct = new Product(Producten.Count+1, product, prijs, voorraad);

                        Producten.Add(nieuwProduct);

                        nieuwProduct.ToonInfo();
                            i++;

                        break;
                    case 2:
                        
                        if (Producten.Count == 0) Console.WriteLine($"er zijn geen producten");
                        else 
                        {
                        foreach (Product p in Producten) { p.ToonInfo(); }
                        }
                            break;
                }
                
            }
            Console.ReadKey();
        }
    }
}
