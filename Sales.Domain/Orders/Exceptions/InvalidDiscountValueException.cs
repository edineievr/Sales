using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Domain.Orders.Exceptions
{
    public class InvalidDiscountValueException : DomainException
    {
        public InvalidDiscountValueException(decimal discountValue) : base($"The discount value '{discountValue}' is invalid. It must be greater than zero.")
        {
        }
    }
}
