using Observer.Observer;
using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Notificar os cliente que o produto chegou 
            var gustavo = new Observador("Gustavo");
            var pedro = new Observador("Pedro");

            var product = new Product();            
            product.Subscribe(pedro);

            Console.WriteLine("");
            Console.WriteLine("------------------");
            Console.WriteLine("");

            var products = GetProducts();
            foreach (var item in products)
            {
                if ("Livros" == item)
                    product.Subscribe(gustavo);
                else
                    product.UnSubscribe(gustavo);

                product.AddProductNew(item);                
            }
            

        }

        public static List<string> GetProducts()
        {
            return new List<string>()
            {
                "Whey Isolado!",
                "PS5",
                "Notebook",
                "Piscina",
                "Livros"
            };
        }
    }
}
