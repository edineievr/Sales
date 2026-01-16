using Sales.Domain.Exceptions;
using Sales.Domain.Orders.Entities.Enums;

namespace Sales.Domain.Orders.Exceptions
{
    public class OrderIsNotEditableException : DomainException
    {
        public OrderIsNotEditableException(OrderStatus status) : base($"Order cannot be modified when status is '{status}'.")
        {

        }
    }

}
