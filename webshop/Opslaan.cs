using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace webshop
{
    internal class Opslaan
    {
            //Dit slaat de product op in een textfile.
            static void OpslaanProduct(string naam, double prijs)
            {
                string inhoud = $"{naam};{prijs}";
                File.AppendAllLines("producten.txt", new[] { inhoud });
            }
            //Dit slaat de klant op in een textfile.
            static void OpslaanKlant(string naam, string email)
            {
                string inhoud = $"{naam};{email}";
                File.AppendAllLines("klanten.txt", new[] { inhoud });
            }
            //Dit slaat de bestelling op in een textfile.
            static void OpslaanBestelling(string orderId, string product, int aantal)
            {
                string inhoud = $"{DateTime.Now};{orderId};{product};{aantal}";
                File.AppendAllLines("bestellingen.txt", new[] { inhoud });
            }
        }
        
    
}
