using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class DiscountExceedsAmountException : DomainException
    {
        public DiscountExceedsAmountException(decimal discountValue)
            : base($"The discount value: {discountValue} exceeds the amount value.")
        {
        }
    }
}
