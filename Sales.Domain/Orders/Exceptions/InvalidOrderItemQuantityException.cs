using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class InvalidOrderItemQuantityException : DomainException
    {
        public InvalidOrderItemQuantityException(decimal quantity) : base($"Order item quantity must be greater than zero. Provided: {quantity}")
        {
        }
    }
}
