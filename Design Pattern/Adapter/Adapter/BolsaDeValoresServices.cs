using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public class BolsaDeValoresServices
    {
        private readonly IDadosDaBolsaDeValores _dadosDaBolsaDeValores;

        public BolsaDeValoresServices(IDadosDaBolsaDeValores dadosDaBolsaDeValores)
        {
            _dadosDaBolsaDeValores = dadosDaBolsaDeValores;
        }

        public string GerarRelatorio()
        {
            return _dadosDaBolsaDeValores.GerarRelatorio();
        }
    }
}
