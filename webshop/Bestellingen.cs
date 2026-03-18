using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Bestellingen
    {
        int id;
        double prijs;
        int aantal;
        string stad;
        string straat;
        public string Straat { get; set; }
        public string Stad { get; set; }
        public string Naam { get; set; }
        public int Id { get; set; }
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
        public int Aantal
        {
            get
            {
                return aantal;
            }
            set
            {
                if (value > 0) aantal = value;
                else aantal = 0;
            }
        }
        public Bestellingen(int id,string stad, string straat, string naam, double prijs, int aantal)
        {
            Id = id;
            Stad = stad;
            Straat = straat;
            Naam = naam;
            Prijs = prijs;
            Aantal = aantal;
        }

        public void ToonInfo()
        {
            Console.WriteLine($"product: {Naam}");
            Console.WriteLine($"kost: {Prijs * Aantal}");
            Console.WriteLine($"aantal: {Aantal}");
            Console.WriteLine($"stad: {Stad}");
            Console.WriteLine($"straat {Straat}");

        }
    }
}
