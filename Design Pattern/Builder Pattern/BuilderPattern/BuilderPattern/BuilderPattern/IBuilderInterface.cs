using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderPattern.BuilderPattern
{

    public interface ICustomerName
    {
        ICustomerPhoneNumber WithCustomerName(string name);
    }

    public interface ICustomerPhoneNumber
    {
        ICustomerEmailAddress WithCustomerPhoneNumber(string phoneNumber);
    }

    public interface ICustomerEmailAddress
    {
        ICustomerShippingAddress WithCustomerEmailAddress(string emailAddress);
    }

    public interface ICustomerShippingAddress
    {
        ISalesOrderLineItems WithCustomerShippingAddress(string address);
    }

    public interface ISalesOrderLineItems
    {
        ISalesOrderOptionalValues WithLineItems(IEnumerable<string> lineItems);
    }

    public interface ISalesOrderOptionalValues
    {
        ISalesOrderOptionalValues ShouldWrapAsGift(bool wrap);
        SalesOrder Finish();
    }
}
