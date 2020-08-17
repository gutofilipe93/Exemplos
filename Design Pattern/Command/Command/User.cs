using Command;
using Command.CButton;
using System;
using System.Collections.Generic;
using System.Text;

namespace Command
{
    public class User
    {
        private readonly List<ICommand> _commands = new List<ICommand>();
        
        public void AddButton(Button button, string action)
        {
            ICommand command = new ButtonCommand(button, action);
            _commands.Add(command);
        }

        public void Run()
        {
            foreach (var command in _commands)
            {
                command.Run();
            }
        }

    }
}
