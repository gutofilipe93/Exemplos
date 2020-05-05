using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP.Sure.PositionType
{
    public class PositionDeveloper : Position
    {
        public override decimal PercentageDiscount()
        {
            return  0.3M;
        }
    }
}
