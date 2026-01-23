using Sales.Application.Exceptions;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Helpers
{
    public static class DiscountParser
    {
        public static Discount Parse(decimal discountValue, string discountTypeStr)
        {
            if (!Enum.TryParse<DiscountType>(discountTypeStr, ignoreCase: true, out var discountType))
            {
                throw new InvalidDiscountTypeException($"Invalid discount type: {discountTypeStr}");
            }
            return new Discount(discountValue, discountType);
        }
    }
}
