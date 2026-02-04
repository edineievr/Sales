using Sales.Domain.Orders.Exceptions;
using Sales.Domain.Orders.ValueObjects;

namespace Sales.Domain.Orders.Entities
{
    public class OrderItem
    {
        public long Id { get; protected set; }
        public long OrderId { get; protected set; }
        public long ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Discount? Discount { get; private set; }
        public decimal TotalValue => CalculateTotalValue();

        public OrderItem(long productId, decimal unitPrice, decimal quantity)
        {
            if(unitPrice <= 0)
            {
                throw new InvalidOrderItemUnitPriceException(unitPrice);
            }

            if (quantity <= 0)
            {
                throw new InvalidOrderItemQuantityException(quantity);
            }

            if (productId <= 0)
            {
                throw new InvalidOrderItemProductIdException(productId);
            }

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void ApplyDiscountInternal(Discount discount)//todo: add validation to discounts
        {
            Discount = discount;
        }

        public void RemoveDiscount()
        {
            Discount = null;
        }

        public decimal CalculateTotalValue()
        {
            var grossPrice = CalculateGrossPrice();

            return Discount is null ? grossPrice : Discount.ApplyDiscount(grossPrice);
        }

        public decimal CalculateGrossPrice()
        {
            return UnitPrice * Quantity;
        }

        public bool HasDiscount()
        {
            return Discount != null;
        }
    }
}


