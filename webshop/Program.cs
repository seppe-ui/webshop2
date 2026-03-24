using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static webshop.Databeheer;
using System.IO;

namespace webshop
{
    internal class Program
    {
        static List<Product> Producten = new List<Product>();
        static List<Klanten> klanten = new List<Klanten>();
        static List<Bestellingen> bestellingen = new List<Bestellingen>();

        static void Main(string[] args)
        {
            InitialiseerData();
            bool Switch = true;

            while (Switch == true)
            {
                Console.WriteLine("\n--- WEBSHOP SYSTEEM ---");
                Console.WriteLine("1   Product toevoegen");
                Console.WriteLine("2   Product bekijken");
                Console.WriteLine("3   Product aanpassen");
                Console.WriteLine("4   Klant toevoegen");
                Console.WriteLine("5   Klanten bekijken");
                Console.WriteLine("6   Bestelling maken");
                Console.WriteLine("7   Bestelling bekijken");
                Console.WriteLine("8   Zoek product");
                Console.WriteLine("9   Zoek klant");
                Console.WriteLine("10  Zoek bestelling");
                Console.WriteLine("11  Product verwijderen");
                Console.WriteLine("12  Klant verwijderen");
                Console.WriteLine("13  Bestelling verwijderen");
                Console.WriteLine("14  Gegevens OPSLAAN naar .txt");
                Console.WriteLine("15  Gegevens LADEN en TONEN uit .txt");
                Console.WriteLine("16  ALLE opgeslagen gegevens Wissen");
                Console.WriteLine("17  Statistieken (producten, klanten, bestellingen)");
                Console.WriteLine("18  Programma afsluiten");
                Console.WriteLine("--------------------------------");

                int nummer = int.Parse(Console.ReadLine());
                switch (nummer)
                {
                    case 1:
                        Productentoevoegen();
                        break;
                    case 2:
                        Productenbekijken();
                        break;
                    case 3:
                        Productenaanpassen();
                        break;
                    case 4:
                        Klantentoevoegen();
                        break;
                    case 5:
                        Klantenbekijken();
                        break;
                    case 6:
                        Bestellingenmaken();
                        break;
                    case 7:
                        Bestellingenbekijken();
                        break;
                    case 8:
                        ZoekProduct();
                        break;
                    case 9:
                        ZoekKlant();
                        break;
                    case 10:
                        ZoekBestelling();
                        break;
                    case 11: 
                        ProductVerwijderen();
                        break;
                    case 12: KlantVerwijderen();
                        break;
                    case 13: BestellingVerwijderen();
                        break;
                    case 14:
                        TeSavenProducten();
                        TeSavenKlanten();
                        TeSavenBestellingen();
                        break;
                    case 15:
                        OpgeslagenGegevens();
                        break;
                    case 16:
                        AllesWissen();
                        break;
                    case 17:
                        Statistiekentonen();
                        break;
                    case 18:
                        Afsluiten();
                        Switch = false;
                        break;
                }
            }
            Console.ReadKey();
        }
        //Alle code over Producten.
        static public void Productentoevoegen() 
        {

            Console.Write("Welk product wil je toevoegen: ");
            string product = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Wat is de prijs van het product: ");
            double prijs = double.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write("Wat is de voorraad van het product: ");
            int voorraad = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Product nieuwProduct = new Product(Producten.Count + 1, product, prijs, voorraad);

            Producten.Add(nieuwProduct);

            Console.WriteLine("Product Toegevoegd!");
        }
        static public void Productenbekijken() 
        {
            if (Producten.Count == 0)
                Console.WriteLine($"Er zijn geen producten");
            else
            {
                Console.WriteLine();
                foreach (Product p in Producten) { p.ToonInfo(); }
            }
        }
        static public void TeSavenProducten()
        {
            List<string> teSavenProducten = new List<string>();
            foreach (var p in Producten)
            {
                teSavenProducten.Add($"{p.Id};{p.Naam};{p.Prijs};{p.Voorraad}");
            }
            File.WriteAllLines("producten.txt", teSavenProducten);

            Console.WriteLine("Alle producten zijn succesvol opgeslagen in producten.txt!");
        }
        static public void Productenaanpassen() 
        {
            Console.Write("Voer het ID in van het product dat je wilt aanpassen: ");
            int zoekId = int.Parse(Console.ReadLine());

            Product teAanpassen = Producten.Find(p => p.Id == zoekId);

            if (teAanpassen == null)
            {
                Console.WriteLine("Product met dit ID niet gevonden.");
            }
            else
            {
                Console.WriteLine($"Gevonden: {teAanpassen.Naam}");

                Console.Write("Nieuwe naam: ");
                teAanpassen.Naam = Console.ReadLine();

                Console.Write("Nieuwe prijs: ");
                teAanpassen.Prijs = double.Parse(Console.ReadLine());

                Console.Write("Nieuwe voorraad: ");
                teAanpassen.Voorraad = int.Parse(Console.ReadLine());

                Console.WriteLine("Product succesvol aangepast!");
            }
        }
        static public void ZoekProduct()
        {
            Console.Write("Voer (een deel van) de productnaam in: ");
            string zoekterm = Console.ReadLine().ToLower();

            var resultaten = Producten.FindAll(p => p.Naam.ToLower().Contains(zoekterm));

            Console.WriteLine("\n--- Gevonden Producten ---");
            if (resultaten.Count > 0)
            {
                foreach (var p in resultaten) p.ToonInfo();
            }
            else Console.WriteLine("Geen producten gevonden met deze naam.");
        }
        static public void ProductVerwijderen()
        {
            Productenbekijken();
            Console.Write("\nVoer het ID in van het product dat u wilt verwijderen: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int verwijderd = Producten.RemoveAll(p => p.Id == id);
                if (verwijderd > 0)
                {
                    Console.WriteLine("Product succesvol verwijderd.");
                    TeSavenProducten(); // Sla direct op naar producten.txt
                }
                else Console.WriteLine("Product met dit ID niet gevonden.");
            }
        }
        static public void MeestVerkochteProducten()
        {
            Console.WriteLine("\n--- TOP VERKOCHTE PRODUCTEN ---");

            var topProducten = bestellingen
                .GroupBy(b => b.Naam)
                .Select(g => new { Product = g.Key, TotaalVekocht = g.Sum(b => b.Aantal) })
                .OrderByDescending(x => x.TotaalVekocht);

            if (!topProducten.Any())
            {
                Console.WriteLine("Nog geen verkopen geregistreerd.");
                return;
            }

            foreach (var item in topProducten)
            {
                Console.WriteLine($"- {item.Product}: {item.TotaalVekocht} keer verkocht");
            }
        }

        //Alle code over klanten.
        static public void Klantentoevoegen()
        {
            Console.Write("Wat is de naam van de klant: ");
            string naam = Console.ReadLine();

            Console.Write("Wat is de email van de klant: ");
            string email = Console.ReadLine();

            Console.Write("Stad: ");
            string stad = Console.ReadLine();

            Console.Write("Straatnaam + nummer: ");
            string straat = Console.ReadLine();

            Klanten nieuwklanten = new Klanten(klanten.Count + 1, naam, email, stad, straat);

            klanten.Add(nieuwklanten);

            Console.WriteLine("Klant is succesvol toegevoegd!");
            Console.WriteLine();
        }
        static public void Klantenbekijken() 
        {
            if (klanten.Count == 0)
                Console.WriteLine($"Er zijn geen klanten!");
            else
            {
                Console.WriteLine();
                foreach (Klanten k in klanten) { k.ToonInfo(); }
            }
        }
        static public void TeSavenKlanten()
        {
            List<string> dataLijnen = new List<string>();
            foreach (var k in klanten)
            {
                dataLijnen.Add($"{k.Id};{k.Naam};{k.Email};{k.Stad};{k.Straat}");
            }
            File.WriteAllLines("klanten.txt", dataLijnen);
            Console.WriteLine("Klanten succesvol opgeslagen in klanten.txt!");
        }
        static public void ZoekKlant()
        {
            Console.Write("Voer het e-mailadres van de klant in: ");
            string zoekEmail = Console.ReadLine().ToLower();

            var resultaten = klanten.FindAll(k => k.Email.ToLower().Contains(zoekEmail));

            Console.WriteLine("\n--- Gevonden Klanten ---");
            if (resultaten.Count > 0)
            {
                foreach (var k in resultaten) k.ToonInfo();
            }
            else Console.WriteLine("Geen klant gevonden met dit e-mailadres.");
        }
        static public void KlantVerwijderen()
        {
            Klantenbekijken();
            Console.Write("\nVoer het ID in van de klant die u wilt verwijderen: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int verwijderd = klanten.RemoveAll(k => k.Id == id);
                if (verwijderd > 0)
                {
                    Console.WriteLine("Klant succesvol verwijderd.");
                    TeSavenKlanten(); 
                }
                else Console.WriteLine("Klant met dit ID niet gevonden.");
            }
        }

        //Alle code over Bestellingen.
        static public void Bestellingenmaken()
        {
            if (Producten.Count == 0 || klanten.Count == 0)
            {
                Console.WriteLine("Fout: Zorg dat er zowel producten als klanten in het systeem staan.");
                return;
            }

            Productenbekijken();
            Console.Write("\nVoer het ID in van het product dat u wilt bestellen: ");
            int prodId = int.Parse(Console.ReadLine());
            Product gekozenProduct = Producten.Find(p => p.Id == prodId);

            if (gekozenProduct == null)
            {
                Console.WriteLine("Product niet gevonden!");
                return;
            }

            if (gekozenProduct.Voorraad < 5 && gekozenProduct.Voorraad > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine($"WAARSCHUWING: Lage voorraad! Er zijn er nog maar {gekozenProduct.Voorraad} over.");
                Console.ResetColor();
            }
            else if (gekozenProduct.Voorraad <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FOUT: Dit product is uitverkocht en kan niet besteld worden.");
                Console.ResetColor();
                return;
            }

            Console.Write($"Hoeveel stuks van '{gekozenProduct.Naam}' wilt u bestellen? ");
            int aantal = int.Parse(Console.ReadLine());

            if (aantal > gekozenProduct.Voorraad)
            {
                Console.WriteLine($"FOUT: Onvoldoende voorraad. U probeert {aantal} te bestellen, maar er zijn er slechts {gekozenProduct.Voorraad}.");
                return;
            }
            if (aantal <= 0)
            {
                Console.WriteLine("FOUT: Aantal moet groter zijn dan 0.");
                return;
            }

            Klantenbekijken();
            Console.Write("\nVoer het ID in van de klant: ");
            int klantId = int.Parse(Console.ReadLine());
            Klanten gekozenKlant = klanten.Find(k => k.Id == klantId);

            if (gekozenKlant == null)
            {
                Console.WriteLine("Klant niet gevonden!");
                return;
            }

            double prijsExcl = gekozenProduct.Prijs * aantal;

            double totaalInclBtw = prijsExcl * 1.21;

            gekozenProduct.Voorraad -= aantal;

            Bestellingen nieuweBestelling = new Bestellingen(
                bestellingen.Count + 1,
                gekozenKlant.Stad,
                gekozenKlant.Straat,
                gekozenKlant.Naam,
                gekozenProduct.Naam,
                totaalInclBtw,
                aantal
            );

            bestellingen.Add(nieuweBestelling);

            Console.WriteLine("\n========================================");
            Console.WriteLine("  BESTELLING SUCCESVOL AFGEROND!");
            Console.WriteLine($"  Nieuwe voorraad van {gekozenProduct.Naam}: {gekozenProduct.Voorraad}");
            Console.WriteLine("========================================\n");
        }
        static public void Bestellingenbekijken() 
        {
            if (bestellingen.Count == 0) { Console.WriteLine("er zijn geen bestellingen gevonden!"); }
            else foreach (Bestellingen b in bestellingen) { b.ToonInfo(); }
        }
        static public void TeSavenBestellingen()
        {
            List<string> dataLijnen = new List<string>();
            foreach (var b in bestellingen)
            {
                dataLijnen.Add($"{b.Id};{b.Stad};{b.Straat};{b.Klant};{b.Naam};{b.Prijs};{b.Aantal}");
            }
            File.WriteAllLines("bestellingen.txt", dataLijnen);
            Console.WriteLine("Bestellingen succesvol opgeslagen in bestellingen.txt!");
        }
        static public void ZoekBestelling()
        {
            Console.Write("Voer een naam in (Klant of Product): ");
            string zoekterm = Console.ReadLine().ToLower();

            var resultaten = bestellingen.FindAll(b =>
                b.Klant.ToLower().Contains(zoekterm) ||
                b.Naam.ToLower().Contains(zoekterm));

            Console.WriteLine("\n--- Gevonden Bestellingen ---");
            if (resultaten.Count > 0)
            {
                foreach (var b in resultaten) b.ToonInfo();
            }
            else Console.WriteLine("Geen bestellingen gevonden voor deze zoekterm.");
        }
        static public void BestellingVerwijderen()
        {
            Bestellingenbekijken();
            Console.Write("\nVoer het ID in van de bestelling die u wilt verwijderen: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                int verwijderd = bestellingen.RemoveAll(b => b.Id == id);
                if (verwijderd > 0)
                {
                    Console.WriteLine("Bestelling succesvol geannuleerd/verwijderd.");
                    TeSavenBestellingen(); 
                }
                else Console.WriteLine("Bestelling niet gevonden.");
            }
        }
        static public void BestellingenPerKlant()
        {
            Console.WriteLine("\n--- BESTELLINGEN PER KLANT ---");

            var klantStats = bestellingen
                .GroupBy(b => b.Klant)
                .Select(g => new { Klant = g.Key, AantalBestellingen = g.Count() })
                .OrderByDescending(x => x.AantalBestellingen);

            if (!klantStats.Any())
            {
                Console.WriteLine("Nog geen bestellingen gevonden.");
                return;
            }

            foreach (var stat in klantStats)
            {
                Console.WriteLine($"- {stat.Klant}: {stat.AantalBestellingen} bestelling(en)");
            }
        }

        //Alle code over opgeslagen gegevens.
        static public void OpgeslagenGegevens()
        {
            Console.WriteLine("\n[ PRODUCTEN ]");
            if (File.Exists("producten.txt"))
            {
                string[] lijnen = File.ReadAllLines("producten.txt");
                foreach (string lijn in lijnen)
                {
                    Console.WriteLine(lijn);
                }
            }
            else
            {
                Console.WriteLine("Geen producten bestand gevonden.");
            }
            Console.WriteLine("\n[ KLANTEN ]");
            if (File.Exists("klanten.txt"))
            {
                foreach (string lijn in File.ReadAllLines("klanten.txt")) Console.WriteLine(lijn);
            }
            else Console.WriteLine("Geen klantenbestand gevonden.");

            Console.WriteLine("\n[ BESTELLINGEN ]");
            if (File.Exists("bestellingen.txt"))
            {
                foreach (string lijn in File.ReadAllLines("bestellingen.txt")) Console.WriteLine(lijn);
            }
            else Console.WriteLine("Geen bestellingen gevonden.");
        }
        static public void InitialiseerData()
        {
            if (File.Exists("producten.txt"))
            {
                string[] lijnen = File.ReadAllLines("producten.txt");
                foreach (string lijn in lijnen)
                {
                    if (!string.IsNullOrWhiteSpace(lijn))
                    {
                        string[] p = lijn.Split(';');
                        Product temp = new Product(int.Parse(p[0]), p[1], double.Parse(p[2]), int.Parse(p[3]));
                        Producten.Add(temp);
                    }
                }
            }

            if (File.Exists("klanten.txt"))
            {
                string[] lijnen = File.ReadAllLines("klanten.txt");
                foreach (string lijn in lijnen)
                {
                    if (!string.IsNullOrWhiteSpace(lijn))
                    {
                        string[] k = lijn.Split(';');
                        Klanten tempKlant = new Klanten(int.Parse(k[0]), k[1], k[2], k[3], k[4]);
                        klanten.Add(tempKlant);
                    }
                }
            }
            if (File.Exists("bestellingen.txt"))
            {
                string[] lijnen = File.ReadAllLines("bestellingen.txt");
                foreach (string lijn in lijnen)
                {
                    if (!string.IsNullOrWhiteSpace(lijn))
                    {
                        string[] b = lijn.Split(';');

                        Bestellingen temp = new Bestellingen(
                            int.Parse(b[0]),
                            b[1],
                            b[2],
                            b[3],
                            b[4],
                            double.Parse(b[5]),
                            int.Parse(b[6])
                        );

                        bestellingen.Add(temp);
                    }
                }
            }
        Console.WriteLine("--- DATA AUTOMATISCH GELADEN ---");
            ToonDashboard();
            Console.WriteLine("--------------------------------");
        }
        static public void AllesWissen()
        {
            Console.WriteLine("Weet je zeker dat je ALLE gegevens wilt wissen? (ja/nee)");
            string bevestiging = Console.ReadLine().ToLower();

            if (bevestiging == "ja")
            {
                if (File.Exists("producten.txt")) File.Delete("producten.txt");
                if (File.Exists("klanten.txt")) File.Delete("klanten.txt");
                if (File.Exists("bestellingen.txt")) File.Delete("bestellingen.txt");

                Producten.Clear();
                klanten.Clear();

                Console.WriteLine("Alle bestanden zijn verwijderd. Herstart het programma voor schone testdata.");
            }
        }

        //Design van de dashboard.
        static public void ToonDashboard()
        {
            Console.Clear();
            string scheiding = new string('-', 95); 

            Console.WriteLine(scheiding);
            Console.WriteLine("| {0,-25} | {1,-30} | {2,-30} |", "KLANTEN", "PRODUCTEN", "BESTELLINGEN");
            Console.WriteLine(scheiding);

            int maxRows = Math.Max(klanten.Count, Math.Max(Producten.Count, bestellingen.Count));

            for (int i = 0; i < maxRows; i++)
            {
                string klantInfo = (i < klanten.Count) ? $"{klanten[i].Id}. {klanten[i].Naam}" : "";
                string productInfo = (i < Producten.Count) ? $"{Producten[i].Id}. {Producten[i].Naam} ({Producten[i].Prijs:N2})" : "";
                string bestelInfo = (i < bestellingen.Count) ? $"#{bestellingen[i].Id} {bestellingen[i].Klant} ({bestellingen[i].Aantal}x)" : "";

                Console.WriteLine("| {0,-25} | {1,-30} | {2,-30} |", klantInfo, productInfo, bestelInfo);

            }

            Console.WriteLine(scheiding);
            Console.WriteLine(" Gebruik het menu hieronder om acties uit te voeren:");
        }

        //Statistieken.
        static public void Statistiekentonen()
        {
            Console.Clear();
            Console.WriteLine("========== WINKEL STATISTIEKEN ==========");

            Console.WriteLine($"Totaal aantal producten: {Producten.Count}");
            Console.WriteLine($"Totaal aantal klanten:   {klanten.Count}");
            Console.WriteLine($"Totaal aantal orders:    {bestellingen.Count}");

            double omzet = bestellingen.Sum(b => b.Prijs);
            Console.WriteLine($"Totale omzet (incl. BTW): {omzet:N2}");

            MeestVerkochteProducten();
            BestellingenPerKlant();

            Console.WriteLine("=========================================");
            Console.WriteLine("\nDruk op een toets om terug te gaan naar het menu...");
            Console.ReadKey();
        }

        //Afsluiten van de console.
        static public void Afsluiten()
        {
            Console.WriteLine("Systeem wordt afgesloten. Tot ziens!");
        }
    }
}