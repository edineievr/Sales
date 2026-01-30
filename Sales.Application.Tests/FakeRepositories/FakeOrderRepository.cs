using Sales.Application.Contracts.Repositories;
using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Exceptions;

namespace Sales.Application.Tests.FakeRepositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders;
        public FakeOrderRepository()
        {
            _orders = [];
        }

        public IReadOnlyCollection<Order> GetAll()
        {
            return _orders;

        }

        public void InsertOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetById(long id)
        {
            return _orders.FirstOrDefault(order => order.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            var index = _orders.FindIndex(o => o.Id == order.Id);

            if (index == -1)
                throw new OrderNotFoundException(order.Id);

            _orders[index] = order;
        }

        public void DeleteOrder(long id)
        {
            _orders.RemoveAll(order => order.Id == id);
        }
    }
}
