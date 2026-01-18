using Sales.Domain.Exceptions;
using Sales.Domain.Orders.Exceptions;
using Sales.Domain.Orders.ValueObjects;

namespace Sales.Domain.Orders.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public Discount? Discount { get; private set; }
        public decimal TotalPrice => CalculateTotalPrice();

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

        public void ApplyDiscount(Discount discount)
        {
            Discount = discount;
        }

        public decimal CalculateTotalPrice()
        {
            var grossPrice = CalculateGrossPrice();

            return Discount == null ? grossPrice : Discount.ApplyDiscount(grossPrice);
        }

        public decimal CalculateGrossPrice()
        {
            return UnitPrice * Quantity;
        }
    }
}


