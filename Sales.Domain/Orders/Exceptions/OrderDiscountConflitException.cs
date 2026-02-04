using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderDiscountConflictException : DomainException
    {
        public OrderDiscountConflictException()
            : base("Order cannot have a discount when at least one item has a discount.")
        {
        }
    }
}
