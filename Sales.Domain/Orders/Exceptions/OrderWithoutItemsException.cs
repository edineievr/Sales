using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderWithoutItemsException : DomainException
    {
        public OrderWithoutItemsException() : base("The order must have at least one item to be invoiced.")
        {
        }
    }
}
