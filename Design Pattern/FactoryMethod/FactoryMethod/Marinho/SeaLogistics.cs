using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Marinho
{
    public class SeaLogistics : LogisticsFactory
    {
        public override Transport CreateTransport()
        {
            return new Ship();
        }
    }
}
