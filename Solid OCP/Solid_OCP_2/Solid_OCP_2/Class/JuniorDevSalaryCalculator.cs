using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP_2.Class
{
    public class JuniorDevSalaryCalculator : BaseSalaryCalculator
    {
        public JuniorDevSalaryCalculator(DeveloperReport developerReport) : base(developerReport)
        {
        }

        public override double CalculateSalary()
        {
            return _developerReport.WorkingHours * _developerReport.WorkingHours;
        }
    }
}
