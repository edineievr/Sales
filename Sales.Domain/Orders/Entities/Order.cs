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
        public DateTime? CancelationDate { get; private set; }
        public decimal TotalItemsValue => _items.Sum(item => item.TotalValue);//ToDo: move this to a specific method
        public decimal TotalOrderValue { get; private set; }
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
            EnsureIsEditable();
            EnsureOrderHasNoDiscount(); 

            var item = new OrderItem(productId, unitPrice, quantity);

            if (discount != null)
            {
                item.ApplyDiscountInternal(discount);
            }                

            _items.Add(item);

            RecalculateTotal();
        }


        public void RemoveItem(long idItem)
        {
            EnsureIsEditable();

            var item = GetItem(idItem) ?? throw new OrderItemNotFoundException(idItem);

            _items.Remove(item);
        }

        private void EnsureIsEditable()
        {
            if (Status != OrderStatus.Open)
            {
                throw new OrderIsNotEditableException(Status);
            }
                
        }

        public void InvoiceOrder()
        {
            EnsureIsEditable();

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

        public void ApplyOrderDiscount(Discount discount)
        {
            if (_items.Count == 0)
            {
                throw new OrderWithoutItemsException();
            }

            EnsureIsEditable();
            EnsureNoItemsHasDiscount();

            Discount = discount;
            RecalculateTotal();
        }

        public void ApplyItemDiscount(long itemId, Discount discount)
        {
            EnsureIsEditable();
            EnsureOrderHasNoDiscount();
            
            var item = GetItem(itemId);
            
            item.ApplyDiscountInternal(discount);
        }

        public void RemoveItemDiscount(long idItem)
        {
            EnsureIsEditable();
            
            var item = GetItem(idItem);
            
            item.RemoveDiscountInternal();
        }

        public void RemoveOrderDiscount()
        {
            EnsureIsEditable();
            
            Discount = null;
            RecalculateTotal();
        }

        private decimal CalculateTotal()
        {
            var total = TotalItemsValue;

            if (Discount != null)
            {
                total = Discount.ApplyDiscount(total);
            }            

            if (total <= 0)
            {
                throw new OrderTotalMustBeGreaterThanZeroException(total);
            }
            
            return total;
        }

        private void RecalculateTotal()
        {
            TotalOrderValue = CalculateTotal();
        }

        private void EnsureNoItemsHasDiscount()
        {
            if (_items.Any(item => item.HasDiscount()))
            {
                throw new OrderDiscountConflictException();
            }
        }

        private void EnsureOrderHasNoDiscount()
        {
            if (Discount != null)
            {
                throw new OrderDiscountConflictException();
            }
        }
        
        private OrderItem GetItem(long itemId)
        {
            var item = _items.FirstOrDefault(orderItem => orderItem.Id == itemId);

            return item ?? throw new OrderItemNotFoundException(itemId);
        }
    }
}
