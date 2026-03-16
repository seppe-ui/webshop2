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
                Console.WriteLine("\n--- WEBSHOP SYSTEEM ---");
                Console.WriteLine($"1  Product toevoegen");
                Console.WriteLine($"2  Product bekijken");
                Console.WriteLine($"3  Product aanpassen");
                Console.WriteLine($"4  Klant toevoegen");
                Console.WriteLine($"5  Klanten bekijken");
                Console.WriteLine($"6  Bestelling maken");
                Console.WriteLine($"7  Bestelling bekijken");
                Console.WriteLine($"8  Statistieken (producten, klanten, bestellingen)");
                Console.WriteLine($"9  Programma afsluiten");
                int nummer = int.Parse(Console.ReadLine());
                switch (nummer) 
                {
                    case 1:

                        Console.Write("Welk product wil je toevoegen: ");
                        string product = Console.ReadLine();
                        Console.WriteLine();

                        Console.Write("Wat is de prijs van het product: ");
                        double prijs = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Console.Write("Wat is de voorraad van het product: ");
                        int voorraad = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Product nieuwProduct = new Product(Producten.Count+1, product, prijs, voorraad);

                        Producten.Add(nieuwProduct);

                        Console.WriteLine("Product Toegevoegd!");
                            i++;

                        break;
                    case 2:
                        
                        if (Producten.Count == 0) Console.WriteLine($"Er zijn geen producten");
                        else 
                        {
                            Console.WriteLine();
                        foreach (Product p in Producten) { p.ToonInfo(); }
                        }
                            break;
                    case 9:
                        Console.WriteLine("Systeem wordt afgesloten. Tot ziens!");
                        Switch = false;
                        break;
                }                
            }
            Console.ReadKey();
        }
    }
}
