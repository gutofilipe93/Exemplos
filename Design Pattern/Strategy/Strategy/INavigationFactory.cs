using Strategy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy
{
    public interface INavigationFactory
    {
        IRouteStrategy CreateRoute(string type);
    }
}
