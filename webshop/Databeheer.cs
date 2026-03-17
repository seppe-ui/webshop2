using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Databeheer
    {
public class BestandsBeheer
    {
        private string productBestand = "producten.txt";
        private string klantBestand = "klanten.txt";

        public void SlaGegevensOp(List<Product> producten, List<Klanten> klanten)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(productBestand))
                {
                    foreach (var p in producten)
                    {
                        sw.WriteLine($"{p.Id};{p.Naam};{p.Prijs};{p.Voorraad}");
                    }
                }

                using (StreamWriter sw = new StreamWriter(klantBestand))
                {
                    foreach (var k in klanten)
                    {
                        
                        sw.WriteLine($"{k.Id};{k.Naam};{k.Email}");
                    }
                }
                Console.WriteLine("\n[INFO] Alle producten en klanten zijn succesvol opgeslagen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FOUT] Er ging iets mis bij het opslaan: {ex.Message}");
            }
        }

            public List<Product> LaadProducten()
            {
                List<Product> lijst = new List<Product>();
                if (!File.Exists(productBestand)) return lijst;

                foreach (var regel in File.ReadAllLines(productBestand))
                {
                    string[] delen = regel.Split(';');
                    if (delen.Length == 4)
                    {
                        int id = int.Parse(delen[0]);
                        string naam = delen[1];
                        double prijs = double.Parse(delen[2]);
                        int voorraad = int.Parse(delen[3]);

                        lijst.Add(new Product(id, naam, prijs, voorraad));
                    }
                }
                return lijst;
            }

            public List<Klanten> LaadKlanten()
            {
                List<Klanten> lijst = new List<Klanten>();
                if (!File.Exists(klantBestand)) return lijst;

                foreach (var regel in File.ReadAllLines(klantBestand))
                {
                    string[] delen = regel.Split(';');
                    if (delen.Length == 3)
                    {
                        int id = int.Parse(delen[0]);
                        string naam = delen[1];
                        string email = delen[2];

                        lijst.Add(new Klanten(id, naam, email));
                    }
                }
                return lijst;
            }
        }
}
}

