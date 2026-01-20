using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;

namespace Sales.Domain.Orders.ValueObjects
{
    public sealed class Discount
    {
        public decimal Value { get; private set; }
        public DiscountType Type { get; private set; }

        public Discount(decimal discountValue, DiscountType type)
        {
            if (discountValue <= 0)
            {
                throw new InvalidDiscountValueException(discountValue);
            }

            Value = discountValue;
            Type = type;
        }

        public decimal ApplyDiscount(decimal amount)
        {
            return Type switch
            {
                DiscountType.Percentage => amount - (amount * (Value / 100)),
                DiscountType.FixedAmount => amount - Value,
                _ => amount
            };
        }
    }
}
