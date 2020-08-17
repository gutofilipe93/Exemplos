using Adapter.Json;
using Adapter.Xml;
using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine(new BolsaDeValoresServices(new BolsaDeValoresXml()).GerarRelatorio());
            Console.WriteLine(new BolsaDeValoresServices(new BolsaDeValoresAdapter()).GerarRelatorio());
            
            // O Adapter tem como objetivo adaptar duas class ou interfaces que tenhão metodos com a mesma saida, para elas serem usadas pelas mesma interface.
        }
    }
}
