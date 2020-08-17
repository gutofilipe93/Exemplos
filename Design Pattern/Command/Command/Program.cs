using Command.CButton;
using System;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.AddButton(new Button(), "Salvar");
            user.AddButton(new Button(), "DELETAR");
            user.AddButton(new Button(), "ATUALIZAR");
            user.Run();
        }
    }
}
