using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP.Error
{
    public class CalculateSalary
    {
        // O Maior erro é que a classe esta aberta para modificação e fechada para extensão
        // Ou seja se aparecer mais um Cargo vai ter que adicionar um novo IF
        public decimal CalculateDiscountSalary(Employee employee)
        {
            if (employee.Position == "Diretor")            
                return employee.Salary * 0.2M;            
            else if (employee.Position == "Desenvolvimento")
                return employee.Salary * 0.3M;

            return employee.Salary;
        }
    }
}
