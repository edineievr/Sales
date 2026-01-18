using Sales.Domain.Orders.Entities.Enums;
using Sales.Domain.Orders.Exceptions;

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
            Status = OrderStatus.Open;
            CreationDate = DateTime.UtcNow;
        }

        public void AddItem(long productId, decimal unitPrice, decimal quantity)
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            _items.Add(new OrderItem(productId, unitPrice, quantity));
        }

        public void RemoveItem(long idItem)
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            var item = _items.FirstOrDefault(item => item.Id == idItem) ?? throw new OrderItemNotFoundException(idItem);

            _items.Remove(item);
        }

        public bool IsEditable()
        {
            return Status == OrderStatus.Open;
        }

        public void InvoiceOrder()
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            if (_items.Count == 0)
            {
                throw new OrderWithoutItemsException();
            }

            Status = OrderStatus.Invoiced;
            InvoiceDate = DateTime.UtcNow;
        }

        public void ReverseToOpen()
        {
            if (Status != OrderStatus.Invoiced)
            {
                throw new OrderIsNotReversibleException(Status);
            }

            Status = OrderStatus.Open;
        }
    }
}
