using Sales.Domain.Exceptions;
using Sales.Domain.Orders.Exceptions;

namespace Sales.Domain.Orders.Entities
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

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
    }
}


