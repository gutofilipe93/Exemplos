using BuilderPattern.BuilderPattern;
using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var salesOrder = SalesOrderBuilder
                            .Start()
                            .WithCustomerName("Jacob Cunningham")
                            .WithCustomerPhoneNumber("281-555-5555")
                            .WithCustomerEmailAddress("example@example.com")
                            .WithCustomerShippingAddress("123 Awesome Rd. Houston, TX 77084")
                            .WithLineItems(new List<string> { "Headphones" })
                            .ShouldWrapAsGift(true)
                            .Finish();

            Console.WriteLine($"{salesOrder.CustomerName}");
        }
    }
}
