using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Terrestre
{
    public class RoadLogistics : LogisticsFactory
    {
        public override Transport CreateTransport()
        {
            return new Truck();
        }
    }
}
