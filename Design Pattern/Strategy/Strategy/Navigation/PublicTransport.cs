using Strategy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.Navigation
{
    public class PublicTransport : IRouteStrategy
    {
        public string BuidRoute()
        {
            return "O melhor caminho de onibus é pegar o Avelino-Palma";
        }
    }
}
