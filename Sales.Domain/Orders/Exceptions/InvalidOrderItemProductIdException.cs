using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class InvalidOrderItemProductIdException : DomainException
    {
        public InvalidOrderItemProductIdException(long productId) : base($"The ProductId '{productId}' is invalid. It must be greater than zero.")
        {
        }
    }
}
