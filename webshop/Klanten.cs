using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Klanten
    {

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Stad { get; set; }
        public string Straat { get; set; }
        public Klanten(int id, string naam, string email, string stad, string straat)
        {
            Id = id;
            Naam = naam;
            Email = email;
            Stad = stad;
            Straat = straat;
        }


        public void ToonInfo()
        {
            Console.WriteLine($"\nId: {Id}");
            Console.WriteLine($"Naam: {Naam}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Stad: {Stad}");
            Console.WriteLine($"Straat: {Straat}");
            Console.WriteLine();
        }

    }
}