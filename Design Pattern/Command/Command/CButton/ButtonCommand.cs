using System;
using System.Collections.Generic;
using System.Text;

namespace Command.CButton
{
    public class ButtonCommand : ICommand
    {

        // Esse pattern é usado para adicionar objetos(Atributos e metodos), para conseguir executar seus metodos da forma que melhor desejar;

        private readonly Button _button;
        private readonly string _action;
        public ButtonCommand(Button button, string action)
        {
            _button = button;
            _action = action;
        }

        public void Run()
        {
            _button.Click(_action);
        }
    }
}
