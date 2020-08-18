using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public interface ISubject
    {
        void EnviarEmail();
        void Adicionar(IObserver observer);
        void Remover(IObserver observer);
    }
}
