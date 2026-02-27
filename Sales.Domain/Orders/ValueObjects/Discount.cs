using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;

namespace Sales.Domain.Orders.ValueObjects
{
    public sealed class Discount
    {
        public decimal Value { get; private set; }
        public DiscountType Type { get; private set; }

        public Discount(decimal value, DiscountType type)
        {
            if (value <= 0)
            {
                throw new InvalidDiscountValueException(value);
            }

            Value = value;
            Type = type;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            var discountedAmount = Type switch
            {
                DiscountType.Percentage => amount - (amount * (Value / 100)),
                DiscountType.FixedAmount => amount - Value,
                _ => amount
            };

            if (discountedAmount < 0)
            {
                throw new DiscountExceedsAmountException(Value);
            }

            return discountedAmount;
        }
    }
}
