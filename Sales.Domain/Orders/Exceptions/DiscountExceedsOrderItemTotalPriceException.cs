using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class DiscountExceedsOrderItemValueException : DomainException
    {
        public DiscountExceedsOrderItemValueException(decimal discount) : base($"The discount {discount} applied exceeds the item's total value.")
        {
        }
    }
}
