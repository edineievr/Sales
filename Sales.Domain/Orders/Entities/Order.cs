using Sales.Domain.Orders.Entities.Enums;

namespace Sales.Domain.Orders.Entities
{
    public class Order
    {
        public long Id { get; protected set; }
        private List<OrderItem> _items { get; set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public DateTime CreationDate { get; private set; }
        public DateTime? InvoiceDate { get; private set; }
        public decimal TotalItemsValue => _items.Sum(item => item.UnitPrice * item.Quantity);
        public OrderStatus Status { get; set; }

        public Order()
        {
            _items = [];
            Status = OrderStatus.Pending;
            CreationDate = DateTime.UtcNow;
        }

        public void AddItem(long productId, decimal unitPrice, decimal quantity)
        {
            _items.Add(new OrderItem(productId, unitPrice, quantity));
        }

        public void RemoveItem(long idItem)
        {
            var item = _items.FirstOrDefault(item => item.Id == idItem) ?? throw new Exception("Item not found");

            _items.Remove(item);
        }

    }
}
