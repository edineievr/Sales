namespace Order.Domain.Orders.Entities
{
    public class OrderItem(long productId, decimal unitPrice, decimal quantity)
    {
        public long Id { get; set; }
        public long ProductId { get; private set; } = productId;
        public decimal Quantity { get; private set; } = quantity;
        public decimal UnitPrice { get; private set; } = unitPrice;
    }
}


