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

            order.AddItem(1, 10m, 2m);

            order.InvoiceOrder();

            Should.Throw<OrderIsNotEditableException>(() =>
            {
                order.AddItem(3, 15m, 5m);
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

            Should.Throw<OrderItemNotFoundException>(() =>
            {
                order.RemoveItem(999);
            });
        }

        [Test]
        public void When_AddingInvalidQuantityItem_Should_ThrowException()
        {
            var order = new Order();

            Should.Throw<InvalidOrderItemQuantityException>(() =>
            {
                order.AddItem(1L, 10m, -5m);
            });
        }

        [Test]
        public void When_AddingInvalidUnitPriceItem_Should_ThrowException()
        {
            var order = new Order();

            Should.Throw<InvalidOrderItemUnitPriceException>(() =>
            {
                order.AddItem(1L, -10m, 5m);
            });
        }

        [Test]
        public void When_AddingMultipleItems_Should_CalculateTotalItemsValueCorrectly()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 5m); // Total: 50
            order.AddItem(2L, 5m, 4m);  // Total: 20

            order.TotalItemsValue.ShouldBe(70m);
        }

        [Test]
        public void When_RemovingItem_Should_UpdateTotalItemsValueCorrectly()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 5m); // Total: 50
            order.AddItem(2L, 5m, 4m);  // Total: 20

            var itemIdToRemove = order.Items.First(item => item.ProductId == 1L).Id;
            order.RemoveItem(itemIdToRemove);

            order.TotalItemsValue.ShouldBe(20m);
        }

        [Test]
        public void When_OrderIsInEditableState_Should_ReturnTrue()
        {
            var order = new Order();
            order.IsEditable().ShouldBeTrue();
        }

        [Test]
        public void When_OrderIsInNonEditableState_Should_ReturnFalse()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 2m);
            order.InvoiceOrder();

            order.IsEditable().ShouldBeFalse();
        }

        [Test]
        public void When_InvoicingOrderWithoutItems_Should_ThrowException()
        {
            var order = new Order();

            Should.Throw<OrderWithoutItemsException>(() =>
            {
                order.InvoiceOrder();
            });
        }

        [Test]
        public void When_InvoicingEditableOrderWithItems_Should_SetInvoiceDate()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 2m);
            order.InvoiceOrder();

            order.InvoiceDate.ShouldNotBeNull();
        }

        [Test]
        public void When_InvoicingNonEditableOrder_Should_ThrowException()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);

            order.InvoiceOrder();

            Should.Throw<OrderIsNotEditableException>(() =>
            {
                order.InvoiceOrder();
            });
        }
    }
}
