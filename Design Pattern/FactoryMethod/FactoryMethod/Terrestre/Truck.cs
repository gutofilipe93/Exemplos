using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Terrestre
{
    public class Truck : Transport
    {
        public override string Deliver()
        {
            return "A entrega deve ser feita por terra";
        }
    }
}
