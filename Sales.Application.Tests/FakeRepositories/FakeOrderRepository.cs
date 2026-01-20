using Sales.Application.Contracts.Repositories;
using Sales.Domain.Orders.Entities;

namespace Sales.Application.Tests.FakeRepositories
{
    public class FakeOrderRepository
    {
        private readonly List<Order> _orders;
        public FakeOrderRepository()
        {
            _orders = [];
        }
        
        public void InsertOrder(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrderById(long id)
        {
            return _orders.FirstOrDefault(order => order.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            var index = _orders.FindIndex(o => o.Id == order.Id);

            if (index == -1)
                throw new InvalidOperationException($"Order {order.Id} not found");

            _orders[index] = order;
        }

        public void DeleteOrder(long id)
        {
            _orders.RemoveAll(order => order.Id == id);
        }
    }
}
