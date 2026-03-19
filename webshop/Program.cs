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
                Console.WriteLine($"1  Product toevoegen");
                Console.WriteLine($"2  Product bekijken");
                Console.WriteLine($"3  Product aanpassen");
                Console.WriteLine($"4  Klant toevoegen");
                Console.WriteLine($"5  Klanten bekijken");
                Console.WriteLine($"6  Bestelling maken");
                Console.WriteLine($"7  Bestelling bekijken");
                Console.WriteLine($"8  Statistieken (producten, klanten, bestellingen)");
                Console.WriteLine($"9  Programma afsluiten");
                Console.WriteLine("10 Gegevens OPSLAAN naar .txt");
                Console.WriteLine("11 Gegevens LADEN en TONEN uit .txt");
                Console.WriteLine("12 ALLE opgeslagen gegevens Wissen");
                Console.WriteLine("--------------------------------");

                int nummer = int.Parse(Console.ReadLine());
                switch (nummer)
                {
                    case 1:
                        productentoevoegen();
                        break;
                    case 2:
                        productenbekijken();
                        break;
                    case 3:
                        productenaanpassen();
                        break;
                    case 4:
                        klantentoevoegen();
                        break;
                    case 5:
                        klantenbekijken();
                        break;
                    case 6:
                        bestellingenmaken();
                        break;
                    case 7:
                        bestellingenbekijken();
                        break;
                    case 8:
                        Statistiekentonen();
                        break;
                    case 9:
                        afsluiten();
                        Switch = false; 
                        break;
                    case 10:
                        teSavenProducten();
                        teSavenKlanten();
                        teSavenBestellingen();
                        break;
                    case 11:
                        opgeslagenGegevens();
                        break;
                    case 12:
                        allesWissen();
                        break;
                        
                }
            }
            Console.ReadKey();
        }
        static public void productentoevoegen() 
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
        static public void productenbekijken() 
        {
            if (Producten.Count == 0)
                Console.WriteLine($"Er zijn geen producten");
            else
            {
                Console.WriteLine();
                foreach (Product p in Producten) { p.ToonInfo(); }
            }
        }
        static public void productenaanpassen() 
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
        static public void klantentoevoegen()
        {
            Console.Write("Wat is de naam van de klant: ");
            string naam = Console.ReadLine();

            Console.Write("Wat is de email van de klant: ");
            string email = Console.ReadLine();

            Console.WriteLine($"locatie");
            Console.Write("stad: ");
            string stad = Console.ReadLine();
            Console.Write("straat: ");
            string straat = Console.ReadLine();

            Klanten nieuwklanten = new Klanten(klanten.Count + 1, naam, email, stad, straat);

            klanten.Add(nieuwklanten);

            Console.WriteLine("Klant is succesvol toegevoegd!");
            Console.WriteLine();
        }
        static public void klantenbekijken() 
        {
            if (klanten.Count == 0)
                Console.WriteLine($"Er zijn geen klanten!");
            else
            {
                Console.WriteLine();
                foreach (Klanten k in klanten) { k.ToonInfo(); }
            }
        }
        static public void bestellingenmaken()
        {
            if (Producten.Count == 0 || klanten.Count == 0)
            {
                Console.WriteLine("Zorg dat er zowel producten als klanten bestaan!");
                return;
            }

            productenbekijken();
            Console.Write("\nGeef Product ID in: ");
            int prodId = int.Parse(Console.ReadLine());
            Product findproduct = Producten.Find(p => p.Id == prodId);

            if (findproduct == null) { Console.WriteLine("Product niet gevonden."); return; }

            Console.Write("Hoeveel wil je kopen: ");
            int aantal = int.Parse(Console.ReadLine());

            if (aantal > findproduct.Voorraad) { Console.WriteLine("Niet genoeg voorraad!"); return; }

            klantenbekijken();
            Console.Write("\nGeef Klant ID in: ");
            int klantId = int.Parse(Console.ReadLine());
            Klanten findklant = klanten.Find(k => k.Id == klantId);

            if (findklant == null) { Console.WriteLine("Klant niet gevonden."); return; }

            Bestellingen nieuw = new Bestellingen(bestellingen.Count + 1, findklant.Stad, findklant.Straat, findklant.Naam, findproduct.Naam, findproduct.Prijs * aantal, aantal);
            bestellingen.Add(nieuw);

            findproduct.Voorraad -= aantal;

            Console.WriteLine("\nBestelling succesvol toegevoegd aan de lijst!");
        }

        static public void bestellingenbekijken() 
        {
            if (bestellingen.Count == 0) { Console.WriteLine("er zijn geen bestellingen gevonden!"); }
            else foreach (Bestellingen b in bestellingen) { b.ToonInfo(); }
        }
        static public void Statistiekentonen() 
        {
            Console.WriteLine("statistieken:");
            if (Producten.Count == 0) Console.WriteLine("er zijn nog geen producten.");
            else
            {
                Console.WriteLine("producten");
                foreach (Product p in Producten) { Console.WriteLine($"product: {p.Naam} voorraad: {p.Voorraad}"); }
            }
            if (klanten.Count == 0) { Console.WriteLine("er zijn nog geen klanten."); }
            else
            {
                Console.WriteLine("\nklanten");
                Console.WriteLine($"aantal klanten: {klanten.Count}");
            }
            if (bestellingen.Count == 0) Console.WriteLine("er zijn nog geen bestellingen.");
            else
            {
                Console.WriteLine("\nbestellingen");
                Console.WriteLine($"aantal bestelling: {bestellingen.Count}");
            }
        }
        static public void afsluiten()
        {
            Console.WriteLine("Systeem wordt afgesloten. Tot ziens!");
        }
        static public void teSavenProducten()
        {
            List<string> teSavenProducten = new List<string>();
            foreach (var p in Producten)
            {
                teSavenProducten.Add($"{p.Id};{p.Naam};{p.Prijs};{p.Voorraad}");
            }
            File.WriteAllLines("producten.txt", teSavenProducten);

            Console.WriteLine("Alle producten zijn succesvol opgeslagen in producten.txt!");
        }
        static public void teSavenKlanten()
        {
            List<string> dataLijnen = new List<string>();
            foreach (var k in klanten)
            {
                dataLijnen.Add($"{k.Id};{k.Naam};{k.Email};{k.Stad};{k.Straat}");
            }
            File.WriteAllLines("klanten.txt", dataLijnen);
            Console.WriteLine("Klanten succesvol opgeslagen in klanten.txt!");
        }
        static public void teSavenBestellingen()
        {
            List<string> dataLijnen = new List<string>();
            foreach (var b in bestellingen)
            {
                dataLijnen.Add($"{b.Id};{b.Stad};{b.Straat};{b.Klant};{b.Naam};{b.Prijs};{b.Aantal}");
            }
            File.WriteAllLines("bestellingen.txt", dataLijnen);
            Console.WriteLine("Bestellingen succesvol opgeslagen in bestellingen.txt!");
        }
        static public void opgeslagenGegevens()
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
        static void allesWissen()
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
    }
}

