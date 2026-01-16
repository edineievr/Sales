using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class InvalidOrderItemUnitPriceException : DomainException
    {
        public InvalidOrderItemUnitPriceException(decimal unitPrice) : base($"Order item unit price must be greater than zero. Provided: {unitPrice}")
        {
        }
    }
}
