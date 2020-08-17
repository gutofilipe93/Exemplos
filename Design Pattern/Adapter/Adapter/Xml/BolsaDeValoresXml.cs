using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter.Xml
{
    public class BolsaDeValoresXml : IDadosDaBolsaDeValores
    {
        public string GerarRelatorio()
        {
            return "Gera os dados baseados no xml";
        }
    }
}
