namespace Sales.Application.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(long orderId) : base($"Order {orderId} was not found.")
        {
        }
    }
}
