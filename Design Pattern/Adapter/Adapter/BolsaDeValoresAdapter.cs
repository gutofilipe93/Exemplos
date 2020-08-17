using Adapter.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public class BolsaDeValoresAdapter : IDadosDaBolsaDeValores
    {        
        public string GerarRelatorio()
        {
            return new  BolsaDeValoresJson().ProcessasDadosDaBolsaDeValores();
        }
    }
}
