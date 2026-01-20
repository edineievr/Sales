using Sales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class DiscountExceedsOrderValueException : DomainException
    {
        public DiscountExceedsOrderValueException(decimal discountValue)
            : base($"The discount value: {discountValue} exceeds the total order value.")
        {
        }
    }
}
