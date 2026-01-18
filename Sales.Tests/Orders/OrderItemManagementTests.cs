using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Exceptions;
using Shouldly;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class OrderItemManagementTests
    {
        [Test]
        public void When_AddingItemToEditableOrder_Should_Succeed()
        {
            var order = new Order();
            var productId = 1L;
            var unitPrice = 10m;
            var quantity = 2m;

            order.AddItem(productId, unitPrice, quantity);
            order.Items.Count.ShouldBe(1);

            var item = order.Items.First();

            item.ProductId.ShouldBe(productId);
            item.UnitPrice.ShouldBe(unitPrice);
            item.Quantity.ShouldBe(quantity);
        }

        [Test]
        public void When_AddingItemToNonEditableOrder_Should_ThrowException()
        {
            var order = new Order();

            order.InvoiceOrder();

            Should.Throw<OrderIsNotEditableException>(() =>
            {
                order.AddItem(1L, 10m, 2m);
            });
        }

        [Test]
        public void When_RemovingItemFromEditableOrder_Should_Succeed()
        {
            var order = new Order();

            var productId = 1L;
            var unitPrice = 10m;
            var quantity = 2m;

            order.AddItem(productId, unitPrice, quantity);
            var itemId = order.Items.First().Id;

            order.RemoveItem(itemId);
            order.Items.Count.ShouldBe(0);
        }

        [Test]
        public void When_RemovingItemFromNonEditableOrder_Should_ThrowException()
        {
            var order = new Order();

            var productId = 1L;
            var unitPrice = 10m;
            var quantity = 2m;

            order.AddItem(productId, unitPrice, quantity);

            var itemId = order.Items.First().Id;

            order.InvoiceOrder();

            Should.Throw<OrderIsNotEditableException>(() =>
            {
                order.RemoveItem(itemId);
            });
        }

        [Test]
        public void When_RemovingNonExistentItem_Should_ThrowException()
        {
            var order = new Order();
            
            Should.Throw<Exception>(() =>
            {
                order.RemoveItem(999);
            });
        }
    }
}
