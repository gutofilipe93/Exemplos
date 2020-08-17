using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    public class Questao : ElementoDoQuestionario
    {
        public Questao(string descricao) : base(descricao) { }

        public override void Exibir()
        {
            Console.WriteLine("Questão: {0}", Descricao);
        }
    }
}
