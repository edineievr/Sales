namespace Sales.Api.Application.UserCases.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(long orderId) : base($"Order with ID {orderId} was not found.")
        {
        }
    }
}
