using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Entities.Enums;
using Shouldly;

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


    }
}
