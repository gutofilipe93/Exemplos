using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern.BuilderPattern
{
    public class SalesOrderBuilder :
    ICustomerName,
    ICustomerPhoneNumber,
    ICustomerEmailAddress,
    ICustomerShippingAddress,
    ISalesOrderLineItems,
    ISalesOrderOptionalValues
    {

        private string _customerName;
        private string _customerPhoneNumber;
        private string _customerEmailAddress;
        private string _customerShippingAddress;
        private IEnumerable<string> _salesOrderLineItems;
        private bool _wrap;

        private SalesOrderBuilder()
        {
            _wrap = false;
        }

        public static ICustomerName Start()
        {
            return new SalesOrderBuilder();
        }

        public ICustomerPhoneNumber WithCustomerName(string name)
        {
            _customerName = name;
            return this;
        }

        public ICustomerEmailAddress WithCustomerPhoneNumber(string phoneNumber)
        {
            _customerPhoneNumber = phoneNumber;
            return this;
        }

        public ICustomerShippingAddress WithCustomerEmailAddress(string emailAddress)
        {
            _customerEmailAddress = emailAddress;
            return this;
        }

        public ISalesOrderLineItems WithCustomerShippingAddress(string address)
        {
            _customerShippingAddress = address;
            return this;
        }

        public ISalesOrderOptionalValues WithLineItems(IEnumerable<string> lineItems)
        {
            _salesOrderLineItems = lineItems;
            return this;
        }

        public ISalesOrderOptionalValues ShouldWrapAsGift(bool wrap)
        {
            _wrap = wrap;
            return this;
        }

        public SalesOrder Finish()
        {
            return new SalesOrder
            {
                CustomerName = _customerName,
                CustomerPhoneNumber = _customerPhoneNumber,
                CustomerEmailAddress = _customerEmailAddress,
                CustomerShippingAddress = _customerShippingAddress,
                LineItems = _salesOrderLineItems,
                WrapAsGift = _wrap
            };
        }
    }
}
