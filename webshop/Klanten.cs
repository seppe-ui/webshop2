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
        public Klanten(int id, string naam, string email)
        {
            Id = id;
            Naam = naam;
            Email = email;
        }


        public void ToonInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Naam: {Naam}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine();
        }

    }
}