using Sales.Domain.Exceptions;
using Sales.Domain.Orders.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderIsNotCancelableException : DomainException
    {
        public OrderIsNotCancelableException(OrderStatus status)
            : base($"The order with status '{status}' cannot be canceled.")
        {
        }
    }
}
