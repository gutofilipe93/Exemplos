using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern.BuilderPattern
{
    public class SalesOrder
    {
        public SalesOrder()
        {
            LineItems = new List<string>();
        }

        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerShippingAddress { get; set; }
        public IEnumerable<string> LineItems { get; set; }
        public bool WrapAsGift { get; set; }
    }
}
