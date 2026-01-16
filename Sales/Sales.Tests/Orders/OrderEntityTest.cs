using Sales.Domain.Orders.Entities;
using Shouldly;
using Sales.Domain.Orders.Entities.Enums;

namespace Sales.Tests.Orders
{
    [TestFixture]
    public class OrderEntityTest
    {
        [Test]
        public void When_NewOrderIsCreated_Should_StartWithInPendingState()
        {
            var order = new Order();

            order.Status.ShouldBe(OrderStatus.Pending);
        }

        [Test]
        public void When_NewOrderIsCreated_Should_HaveNoItems()
        {
            var order = new Order();

            order.Items.Count.ShouldBe(0);
        }

        [Test]
        public void When_NewOrderIsCreated_Should_TotalItemsValueBeZero()
        {
            var order = new Order();

            order.TotalItemsValue.ShouldBe(0);
        }

    }
}
