using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Bestellingen
    {
        //Dit zijn de properties.
        public int Id { get; set; }
        public string Stad { get; set; }
        public string Straat { get; set; }
        public string Klant { get; set; } 
        public string Naam { get; set; } 
        private double _totaalPrijs;
        public double Prijs 
        {
            get { return _totaalPrijs; }
            set { _totaalPrijs = value <= 0 ? 0 : value; }
        }

        private int _aantal;
        public int Aantal
        {
            get { return _aantal; }
            set { _aantal = value > 0 ? value : 0; }
        }

        public Bestellingen(int id, string stad, string straat, string klant, string naam, double prijs, int aantal)
        {
            Id = id;
            Stad = stad;
            Straat = straat;
            Klant = klant;
            Naam = naam;
            Prijs = prijs; 
            Aantal = aantal;
        }
        //Dit toont de info van de bestelling.
        public void ToonInfo()
        {
            Console.WriteLine($"\n[ Bestelling #{Id} ]");
            Console.WriteLine($"Product: {Naam}");
            Console.WriteLine($"Aantal:  {Aantal}");
            Console.WriteLine($"Totaal:  {Prijs:N2} (incl. 21% BTW)");
            Console.WriteLine($"Klant:   {Klant}");
        }
    }
}