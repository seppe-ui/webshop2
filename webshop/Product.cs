using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webshop
{
    internal class Product
    {
        public int Id { get; set; }
        public int Naam { get; set; }
        public int Prijs { get; set; }
        public int Voorraad { get; set; }
        List<string> Product = new List<string>();


    }
}
