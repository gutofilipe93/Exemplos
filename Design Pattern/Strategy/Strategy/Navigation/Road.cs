using Strategy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.Navigation
{
    public class Road : IRouteStrategy
    {
        public string BuidRoute()
        {
            return "O melhor caminho pela estrada é a Bandeirantes";
        }
    }
}
