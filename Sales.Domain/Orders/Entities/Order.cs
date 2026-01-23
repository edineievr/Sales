using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;
using Sales.Domain.Orders.ValueObjects;

namespace Sales.Domain.Orders.Entities
{
    public class Order
    {
        public long Id { get; protected set; }
        private List<OrderItem> _items { get; set; }
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public DateTime CreationDate { get; private set; }
        public DateTime? InvoiceDate { get; private set; }
        public DateTime? CancelationDate { get; set; }
        public decimal TotalItemsValue => _items.Sum(item => item.TotalValue);
        public decimal TotalOrderValue => CalculateTotalOrderValue();
        public Discount? Discount { get; private set; }
        public OrderStatus Status { get; private set; }

        public Order()
        {
            _items = [];
            Status = OrderStatus.Open;
            CreationDate = DateTime.UtcNow;
        }

        public void AddItem(long productId, decimal unitPrice, decimal quantity, Discount? discount = null)
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            var item = new OrderItem(productId, unitPrice, quantity);

            if (discount != null)
            {
                item.ApplyDiscount(discount);
            }                

            _items.Add(item);
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
            InvoiceDate = null;
        }

        public void CancelOrder()
        { 
            if (Status == OrderStatus.Canceled)
            {
                throw new OrderIsNotCancelableException(Status);
            }

            Status = OrderStatus.Canceled;
            CancelationDate = DateTime.UtcNow;
        }

        public void SetDiscount(Discount discount)
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            if (HasItemLevelDiscounts())
            {
                throw new OrderDiscountConflictException();
            }

            Discount = discount;
        }

        public void RemoveDiscount()
        {
            if (!IsEditable())
            {
                throw new OrderIsNotEditableException(Status);
            }

            Discount = null;
        }

        private decimal CalculateTotalOrderValue()
        {
            var total = TotalItemsValue;

            total = Discount != null ? Discount.ApplyDiscount(total) : total;

            return total > 0 ? total : throw new DiscountExceedsOrderValueException(Discount.Value);
        }

        public bool HasItemLevelDiscounts()
        {
            return _items.Any(item => item.HasDiscount());
        }        
    }
}
