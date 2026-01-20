using Sales.Domain.Orders.Entities;

namespace Sales.Api.Application.UserCases.Contracts
{
    public class IOrderRepository
    {
        public Order GetOrderById(long id);
        public void UpdateOrder(Order order);
    }
}
