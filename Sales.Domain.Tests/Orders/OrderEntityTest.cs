using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;
using Shouldly;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class OrderEntityTest
    {
        [Test]
        public void When_NewOrderIsCreated_Should_StartWithInOpenState()
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
        public void When_NewOrderIsCreated_Should_CancelationDateBeNull()
        {
            var order = new Order();

            order.CancelationDate.ShouldBeNull();
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

        [Test]
        public void When_OrderIsInvoiced_Should_ChangeStatusToInvoiced()
        {
            var order = new Order();
            order.AddItem(1, 10m, 2);

            order.InvoiceOrder();

            order.Status.ShouldBe(OrderStatus.Invoiced);
        }

        [Test]
        public void When_OrderIsCanceled_Should_ChangeStatusToCanceled()
        {
            var order = new Order();

            order.CancelOrder();

            order.Status.ShouldBe(OrderStatus.Canceled);
        }

        [Test]
        public void When_OrderIsCanceled_Should_SetCancelationDate()
        {
            var order = new Order();
            var beforeCancelation = DateTime.UtcNow;

            order.CancelOrder();
            var afterCancelation = DateTime.UtcNow;

            order.CancelationDate.ShouldNotBeNull();
            order.CancelationDate.Value.ShouldBeGreaterThanOrEqualTo(beforeCancelation);
            order.CancelationDate.Value.ShouldBeLessThanOrEqualTo(afterCancelation);
        }

        [Test]
        public void When_CancelOrderAlreadyCanceledOrder_Should_ThrowException()
        {
            var order = new Order();

            order.CancelOrder();

            Should.Throw<OrderIsNotCancelableException>(() =>
            {
                order.CancelOrder();
            });
        }
    }
}
