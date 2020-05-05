using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP_2.Class
{
    public abstract class BaseSalaryCalculator
    {
        protected DeveloperReport _developerReport;

        public BaseSalaryCalculator(DeveloperReport developerReport)
        {
            _developerReport = developerReport;
        }

        public abstract double CalculateSalary();
    }
}
