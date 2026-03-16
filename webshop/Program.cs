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
                        /*
                            Console.Write($"welke product wilt je toevoegen: ");
                        string product = Console.ReadLine();
                        Console.WriteLine($"prijs van de product:");
                        int prijs = int.Parse(Console.ReadLine());
                        Console.WriteLine($"de voorraad van de product:");
                        int voorraad = int.Parse(Console.ReadLine());
                        for (int i; i > Producten.Count; i++ )
                        {
                            Producten[i] = new Product(i, product, prijs, voorraad);
                        }
                }
                        Console.WriteLine($"Id: {Producten.Count}");
                        Console.WriteLine($"Naam: {product}");
                        Console.WriteLine($"Prijs: {prijs}");
                        Console.WriteLine($"Voorraad: {voorraad}");
                        */
                        Console.Write("welke product wilt je toevoegen: ");
                        string product = Console.ReadLine();

                        Console.WriteLine("prijs van de product:");
                        int prijs = int.Parse(Console.ReadLine());

                        Console.WriteLine("de voorraad van de product:");
                        int voorraad = int.Parse(Console.ReadLine());

                        Product nieuwProduct = new Product(Producten.Count, product, prijs, voorraad);

                        Producten.Add(nieuwProduct);

                        Console.WriteLine($"Id: {nieuwProduct.Id}");
                        Console.WriteLine($"Naam: {nieuwProduct.Naam}");
                        Console.WriteLine($"Prijs: {nieuwProduct.Prijs}");
                        Console.WriteLine($"Voorraad: {nieuwProduct.Voorraad}");

                        break;
                    case 2:
                        break;
                }
                
            }
            Console.ReadKey();
        }
    }
}
