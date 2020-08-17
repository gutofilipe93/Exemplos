using System;
using System.Collections.Generic;
using System.Text;

namespace Observer.Observer
{
    public interface IObserver
    {
        string Name { get; }
        void Notification(Product product);
    }
}
