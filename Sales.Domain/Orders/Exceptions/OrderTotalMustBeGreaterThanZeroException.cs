namespace Sales.Domain.Orders.Exceptions;

public class OrderTotalMustBeGreaterThanZeroException : DomainException
{
    public OrderTotalMustBeGreaterThanZeroException(decimal total) : base($"Order total {total}  must be greater than zero.")
    {
    }
}