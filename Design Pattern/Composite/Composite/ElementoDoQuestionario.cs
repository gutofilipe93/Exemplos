using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
    public abstract class ElementoDoQuestionario
    {
        protected string Descricao;

        protected ElementoDoQuestionario(string descricao)
        {
            Descricao = descricao;
        }
        public abstract void Exibir();
    }
}
