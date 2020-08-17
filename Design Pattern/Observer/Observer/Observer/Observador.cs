using System;
using System.Collections.Generic;
using System.Text;

namespace Observer.Observer
{
    public class Observador : IObserver
    {
        public Observador(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Notification(Product product)
        {
            Console.WriteLine("Notificando {0} que {1} " +
                              "chegou na loja ", Name, product.NameProduct);
        }
    }
}
