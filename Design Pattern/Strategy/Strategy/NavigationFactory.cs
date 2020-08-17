using Strategy.Interface;
using Strategy.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy
{
    public class NavigationFactory : INavigationFactory
    {
        public IRouteStrategy CreateRoute(string type)
        {
            switch (type)
            {
                case "P":
                    return new PublicTransport();
                case "E":
                    return new Road();
                case "C":
                    return new Walking();
                default:
                    return null;                    
            }
        }
    }
}
