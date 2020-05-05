using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP.Sure
{
    public class Employee
    {
        public decimal Salary { get; set; }
        public Position Position { get; set; }

        public Employee(decimal salary, Position position)
        {
            Salary = salary;
            Position = position;
        }

        public decimal CalculateSalary()
        {
            return Salary - (Salary * Position.PercentageDiscount());
        }
    }
}
