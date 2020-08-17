using Strategy.Interface;
using System;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            INavigationFactory _navigationFactory = new NavigationFactory();
            IRouteStrategy routeStrategy = _navigationFactory.CreateRoute("C");
            Console.WriteLine(routeStrategy.BuidRoute());
        }
    }
}
