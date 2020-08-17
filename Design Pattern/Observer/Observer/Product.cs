using System;
using System.Collections.Generic;
using System.Text;

namespace Observer.Observer
{
    public class Product
    {

        public string NameProduct { get; set; }
        private readonly List<IObserver> _observers = new List<IObserver>();
        public void Subscribe(IObserver _observer)
        {
            _observers.Add(_observer);
            Console.WriteLine($"Notificando que {_observer.Name} está recebendo atualizãções ");
        }

        public void AddProductNew(string nameProduct)
        {
            NameProduct = nameProduct;
            Notificar();
        }
            

        public void UnSubscribe(IObserver _observer)
        {
            _observers.Remove(_observer);
            Console.WriteLine($"Notificando que {_observer.Name} NÃO está recebendo atualizãções");
        }

        private void Notificar()
        {
            foreach (var investor in _observers)
            {
                investor.Notification(this);
            }

            Console.WriteLine("");
        }
    }
}
