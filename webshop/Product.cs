using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Product
    {
        //Dit zijn de variabellen voor de product.
        double prijs;
        int voorraad;

        //Dit zijn de properties.
        public string Naam { get; set; }
        public int Id {get; set;}
        public double Prijs
        {
            get
            {
                return prijs;
            }
            set
            {
                if (value <= 0) prijs = 0;
                else prijs = value;
            }
        }
        public int Voorraad
        {
            get
            {
                return voorraad;
            }
            set
            {
                if (value > 0) voorraad = value;
                else voorraad = 0;
            } 
        }
        public Product(int id, string naam, double prijs, int voorraad)
        {
            Id = id;
            Naam = naam;
            Prijs = prijs;
            Voorraad = voorraad;
        }
        //Dit toont de info van de product.
        public void ToonInfo()
        {
            Console.WriteLine($"\n[ Product #{Id} ]");

            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Naam: {Naam}");
            Console.WriteLine($"Prijs: {Prijs}");
            Console.WriteLine($"Voorraad: {Voorraad}");
            Console.WriteLine();

        }

    }
}
