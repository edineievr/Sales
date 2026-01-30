using Sales.Domain.Exceptions;

namespace Sales.Domain.Orders.Exceptions;

public class OrderNotFoundException : DomainException
{
    public OrderNotFoundException(long orderId) : base($"Order {orderId} not found")
    {
        
    }
}