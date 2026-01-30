using Sales.Application.Exceptions;
using Sales.Application.Tests.FakeRepositories;
using Sales.Application.UseCases.GetOrder;
using Sales.Domain.Orders.Entities;
using Shouldly;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class GetOrderTest
    {
        [Test]
        public void When_GettingExistingOrder_Should_ReturnOrder()
        {
            var repository = new FakeOrderRepository();

            var order = new Order();
            order.AddItem(1, 100m, 1);

            order.TotalItemsValue.ShouldBe(100m);
            order.TotalOrderValue.ShouldBe(100m);

            repository.InsertOrder(order);

            var handler = new GetOrderHandler(repository);

            var command = new GetOrderQuery
            {
                OrderId = order.Id
            };

            var retrievedOrder = handler.Handle(command);

            retrievedOrder.ShouldNotBeNull();
            retrievedOrder.id.ShouldBe(order.Id);
            retrievedOrder.TotalItemValue.ShouldBe(100m);
            retrievedOrder.TotalOrderValue.ShouldBe(100m);
            retrievedOrder.Items.Count.ShouldBe(1);
        }

        [Test]
        public void When_GettingNonExistentOrder_Should_ShouldThrowException()
        {
            var repository = new FakeOrderRepository();

            var handler = new GetOrderHandler(repository);

            var command = new GetOrderQuery
            {
                OrderId = 999 // Non-existent order ID
            };

            Should.Throw<OrderNotFoundException>(() => handler.Handle(command));


        }
    }
}
