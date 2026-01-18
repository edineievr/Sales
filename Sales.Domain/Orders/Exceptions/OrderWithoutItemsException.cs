using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderWithouItemsException : DomainException
    {
        public OrderWithouItemsException() : base("The order must have at least one item to be invoiced.")
        {
        }
    }
}
