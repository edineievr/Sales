using Sales.Domain.Orders.Entities;

namespace Sales.Application.Contracts.Repositories
{
    public interface IOrderRepository
    {
        public Order GetById(long id);
        public IList<Order> GetAll();
        public void UpdateOrder(Order order);
        public void DeleteOrder(long id);
        public void InsertOrder(Order order);
    }
}
