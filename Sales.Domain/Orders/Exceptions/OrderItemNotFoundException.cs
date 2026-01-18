using Sales.Domain.Exceptions;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderItemNotFoundException : DomainException
    {
        public OrderItemNotFoundException(long itemId) : base($"Order item with ID {itemId} was not found.")
        {
        }
    }
}
