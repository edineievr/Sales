using Sales.Domain.Exceptions;
using Sales.Domain.Orders.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderIsNotReversibleException : DomainException
    {
        public OrderIsNotReversibleException(OrderStatus currentStatus) : base($"Order with status {currentStatus} cannot be reversed to Open.")
        {
        }
    }    
}
