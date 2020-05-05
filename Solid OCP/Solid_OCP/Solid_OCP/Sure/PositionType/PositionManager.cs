using System;
using System.Collections.Generic;
using System.Text;

namespace Solid_OCP.Sure.PositionType
{
    public class PositionManager : Position
    {
        public override decimal PercentageDiscount()
        {
            return  0.2M;
        }
    }
}
