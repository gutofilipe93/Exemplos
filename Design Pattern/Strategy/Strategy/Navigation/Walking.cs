using Strategy.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strategy.Navigation
{
    public class Walking : IRouteStrategy
    {
        public string BuidRoute()
        {
            return "O melhor caminho a pé é ir pela via norte";
        }
    }
}
