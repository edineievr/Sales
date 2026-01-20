using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Shouldly;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class OrderEntityTest
    {
        [Test]
        public void When_NewOrderIsCreated_Should_StartWithInPendingState()
        {
            var order = new Order();

            order.Status.ShouldBe(OrderStatus.Open);
        }

        [Test]
        public void When_NewOrderIsCreated_Should_HaveNoItems()
        {
            var order = new Order();

            order.Items.Count.ShouldBe(0);
        }

        [Test]
        public void When_NewOrderIsCreated_Should_InvoiceDateBeNull()
        {
            var order = new Order();

            order.InvoiceDate.ShouldBeNull();
        }

        [Test]
        public void When_NewOrderIsCreated_Should_HaveCreationDateSet()
        {
            var beforeCreation = DateTime.UtcNow;
            var order = new Order();
            var afterCreation = DateTime.UtcNow;

            order.CreationDate.ShouldBeGreaterThanOrEqualTo(beforeCreation);
            order.CreationDate.ShouldBeLessThanOrEqualTo(afterCreation);
        }

        [Test]
        public void When_NewOrderIsCreated_Should_HaveTotalItemsValueZero()
        {
            var order = new Order();

            order.TotalItemsValue.ShouldBe(0m);
        }
    }
}
