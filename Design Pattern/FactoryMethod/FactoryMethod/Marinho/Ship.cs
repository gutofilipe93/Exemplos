using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod.Marinho
{
    public class Ship : Transport
    {
        public override string Deliver()
        {
            return "Entrega será feita por água";
        }
    }
}
