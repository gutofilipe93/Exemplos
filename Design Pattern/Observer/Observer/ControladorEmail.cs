using System;
using System.Collections.Generic;
using System.Text;

namespace Observer
{
    public class ControladorEmail : ISubject
    {
        private readonly List<IObserver> _observers;

        public ControladorEmail()
        {
            _observers = new List<IObserver>();
        }

        public void Adicionar(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void EnviarEmail()
        {
            foreach (var item in _observers)
            {
                item.EnviarEmail();
            }
        }

        public void Remover(IObserver observer)
        {
            _observers.Remove(observer);
        }
    }
}
