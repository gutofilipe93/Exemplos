using FactoryMethod.Marinho;
using FactoryMethod.Terrestre;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMethod
{
    public abstract class LogisticsFactory
    {
        public abstract Transport CreateTransport();

        public static LogisticsFactory PlantDelivery(string type)
        {
            if (type == "caminhao")
                return new RoadLogistics();
            else
                return new SeaLogistics();
        }
    }
}
