using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var logisticsFactory = LogisticsFactory.PlantDelivery("barco").CreateTransport().Deliver();
            Console.WriteLine(logisticsFactory);
        }
    }
}
