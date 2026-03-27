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
        //Dit zijn de variabellen voor de databeheer.
        private string productBestand = "producten.txt";
        private string klantBestand = "klanten.txt";

        //Dit zorgt ervoor dat alle gegevens woorden opgeslaan in een textfile.
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
            //dit laat de producten van de textfile.
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
            //Dit laat de klanten van de textfile.
            public List<Klanten> LaadKlanten()
            {
                List<Klanten> lijst = new List<Klanten>();
                if (!File.Exists(klantBestand)) return lijst;

                foreach (var regel in File.ReadAllLines(klantBestand))
                {
                    string[] delen = regel.Split(';');
                    if (delen.Length == 5)
                    {
                        int id = int.Parse(delen[0]);
                        string naam = delen[1];
                        string email = delen[2];
                        string stad = delen[3];
                        string straat = delen[4];

                        lijst.Add(new Klanten(id, naam, email, stad, straat));
                    }
                }
                return lijst;
            }
        }
}
}

