using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace webshop
{
    internal class Programa
    {

        static void OpslaanProduct(int id, string naam, double prijs, int voorraad)
{
    string regel = $"{id};{naam};{prijs};{voorraad}";
    File.AppendAllLines("producten.txt", new[] { regel });
}

        static void OpslaanKlant(string naam, string email)
        {
            string inhoud = $"{naam};{email}";
            File.AppendAllLines("klanten.txt", new[] { inhoud });
        }

        static void OpslaanBestelling(string orderId, string product, int aantal)
        {
            string inhoud = $"{DateTime.Now};{orderId};{product};{aantal}";
            File.AppendAllLines("bestellingen.txt", new[] { inhoud });
        }   
}
}
