using Sales.Domain.Orders.Entities;

namespace Sales.Application.Contracts.Repositories
{
    public interface IOrderRepository
    {
        public Order GetOrderById(long id);
        public void UpdateOrder(Order order);
        public void DeleteOrder(long id);
        public long InsertOrder(Order order);
    }
}
