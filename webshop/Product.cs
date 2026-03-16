using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Product
    {
        int id=0;
        double prijs;
        int voorraad;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value >= 0 || value < 3) id = 1;                        
            }
        }
        public string Naam { get; set; }
        public double Prijs
        {
            get
            {
                return prijs;
            }
            set
            {
                if (value >= 0 || value < 1000) prijs = 0;
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
                if (value < 0) voorraad = 0;
            } 
        }
        public Product() : this( 1, "onbekend", 0.1, 1)
        {
        }
        public Product(int id, string naam) : this(id, naam, 0.1, 1)
        {
        }
        public Product(int id, string naam, double prijs, int voorraad)
        {
            Id = id;
            Naam = naam;
            Prijs = prijs;
            Voorraad = voorraad;
        }
        public void ToonInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Naam: {Naam}");
            Console.WriteLine($"Prijs: {Prijs}");
            Console.WriteLine($"Voorraad: {Voorraad}");

        }
    }
}
