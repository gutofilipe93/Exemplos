using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP_2.Class
{
    public class SeniorDevSalaryCalculator : BaseSalaryCalculator
    {
        public SeniorDevSalaryCalculator(DeveloperReport developerReport) : base(developerReport)
        {
        }

        public override double CalculateSalary()
        {
            return _developerReport.HourlyRate * _developerReport.WorkingHours * 1.2;
        }
    }
}
