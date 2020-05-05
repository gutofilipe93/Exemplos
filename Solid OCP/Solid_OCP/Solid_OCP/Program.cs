using Solid_OCP.Sure;
using Solid_OCP.Sure.PositionType;
using System;

namespace Solid_OCP
{
    class Program
    {
        static void Main(string[] args)
        {
            // Forma errada
            //new CalculateSalary().CalculateDiscountSalary(new Employee());

            // Do jeito que esta o codigo não precisa de if quando adicionar um novo cargo
            Position position = new PositionManager();
            Employee employee = new Employee(100, position);
            Console.WriteLine($"Quantidade de salario com desconto {employee.CalculateSalary()}");
            position = new PositionDeveloper();
            employee = new Employee(70, position);
            Console.WriteLine($"Quantidade de salario com desconto {employee.CalculateSalary()}");
        }
    }
}
