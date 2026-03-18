using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static webshop.Databeheer;

namespace webshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> Producten = new List<Product>();
            List<Klanten> klanten = new List<Klanten>();
            List<Bestellingen> bestellingen = new List<Bestellingen>();
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
                Console.WriteLine();
                int nummer = int.Parse(Console.ReadLine());
                switch (nummer) 
                {
                    case 1:

                        Console.Write("Welk product wil je toevoegen: ");
                        string product = Console.ReadLine();
                        Console.WriteLine();

                        Console.Write("Wat is de prijs van het product: ");
                        double prijs = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Console.Write("Wat is de voorraad van het product: ");
                        int voorraad = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Product nieuwProduct = new Product(Producten.Count+1, product, prijs, voorraad);

                        Producten.Add(nieuwProduct);

                        Console.WriteLine("Product Toegevoegd!");

                        break;
                    case 2:
                        
                        if (Producten.Count == 0) 
                            Console.WriteLine($"Er zijn geen producten");
                        else 
                        {
                            Console.WriteLine();
                        foreach (Product p in Producten) { p.ToonInfo(); }
                        }
                            break;
                    case 3:
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
                        break;

                    case 4:
                        Console.Write("Wat is de naam van de klant: ");
                        string naam = Console.ReadLine();

                        Console.Write("Wat is de email van de klant: ");
                        string email = Console.ReadLine();

                        Klanten nieuwklanten = new Klanten(klanten.Count + 1,naam,email);

                        klanten.Add(nieuwklanten);

                        Console.WriteLine("Klant is succesvol toegevoegd!");
                        Console.WriteLine();
                        break;
                    case 5:
                        if (klanten.Count == 0)
                            Console.WriteLine($"Er zijn geen klanten!");
                        else
                        {
                            Console.WriteLine();
                            foreach (Klanten k in klanten) { k.ToonInfo(); }
                        }
                        break;
                    case 6:
                        if (Producten.Count == 0)
                        {
                            Console.WriteLine("er zijn nog geen producten te koop!");
                        }
                        else
                        {   
        
                            foreach (Product p in Producten) { p.ToonInfo(); }
                            Console.WriteLine("");
                            Console.Write($"geef ID in:");
                            int ID = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            if (ID <= 0) Console.WriteLine("Fout geef geen ID's onder 0!");
                            else 
                            {
                                Product findproduct = Producten.Find(p => p.Id == ID);
                                if (findproduct == null)
                                {
                                    Console.WriteLine("Product met dit ID niet gevonden.");
                                }
                                else findproduct.ToonInfo();

                                Console.Write("\nis dit de product?(ja of nee):");
                                string antwoord = Console.ReadLine();

                                if (antwoord == "ja")
                                {
                                    Console.Write("hoeveel wil je kopen:");
                                    int aantal = int.Parse(Console.ReadLine());



                                    if (aantal < findproduct.Voorraad && aantal > 0)
                                    {
                                        Console.WriteLine($"het kost {findproduct.Prijs * aantal} euro");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\ner is niet genoeg voorraad!");
                                        antwoord = "nee";
                                        break;
                                    }
                                    Console.Write($"is dit goed?(ja of nee):");
                                    antwoord = Console.ReadLine();
                                    if (antwoord == "ja")
                                    {
                                        Console.Write("\nstad:");
                                        string stad = Console.ReadLine();

                                        Console.Write("straat:");
                                        string straat = Console.ReadLine();

                                        Bestellingen nieuwbestelling = new Bestellingen(bestellingen.Count + 1, stad, straat, findproduct.Naam, findproduct.Prijs * aantal, aantal);
                                        bestellingen.Add(nieuwbestelling);

                                        Console.WriteLine("\nbestelling is toegevoegd!");
                                    }
                                    
                                }else Console.WriteLine("bestelling is gestopt!");
                            }
                        }
                        break;
                    case 7: 
                        if (bestellingen.Count == 0) { Console.WriteLine("er zijn geen bestellingen gevonden!"); }
                        else foreach (Bestellingen b in bestellingen) { b.ToonInfo(); }
                        break;
                        
                    case 9:
                        Console.WriteLine("Systeem wordt afgesloten. Tot ziens!");
                        Switch = false;
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
