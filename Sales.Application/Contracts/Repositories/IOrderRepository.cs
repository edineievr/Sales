using Sales.Domain.Orders.Entities;

namespace Sales.Application.Contracts.Repositories
{
    public interface IOrderRepository
    {
        public Order GetById(long id);
        public List<Order> GetAll();
        public void Update(Order order);
        public void Delete(Order order);
        public void Insert(Order order);
    }
}
