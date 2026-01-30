using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
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

            order.AddItem(1L, 10m, 2m);
            order.Items.Count.ShouldBe(1);

            var item = order.Items.First();

            item.ProductId.ShouldBe(1L);
            item.UnitPrice.ShouldBe(10m);
            item.Quantity.ShouldBe(2m);
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

            order.AddItem(1L, 10m, 2m);
            var itemId = order.Items.First().Id;

            order.RemoveItem(itemId);
            order.Items.Count.ShouldBe(0);
        }

        [Test]
        public void When_RemovingItemFromNonEditableOrder_Should_ThrowException()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 2m);
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
            order.EnsureIsEditable().ShouldBeTrue();
        }

        [Test]
        public void When_OrderIsInNonEditableState_Should_ReturnFalse()
        {
            var order = new Order();

            order.AddItem(1L, 10m, 2m);
            order.InvoiceOrder();

            order.EnsureIsEditable().ShouldBeFalse();
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

        [Test]
        public void When_InvoicingOrder_Should_ChangeStatusToInvoiced()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);
            order.InvoiceOrder();

            order.Status.ShouldBe(OrderStatus.Invoiced);
        }

        [Test]
        public void When_CancelingOrder_Should_ChangeStatusToCanceled()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);

            order.CancelOrder();

            order.Status.ShouldBe(OrderStatus.Canceled);
        }

        [Test]
        public void When_ReversingToOpenStatus_Should_AllowEditingAgain()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);

            order.InvoiceOrder();
            order.ReverseToOpen();

            order.EnsureIsEditable().ShouldBeTrue();
        }

        [Test]
        public void When_ReversingToOpenStatus_Should_SetStatusToOpen()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);

            order.InvoiceOrder();
            order.ReverseToOpen();

            order.Status.ShouldBe(OrderStatus.Open);
        }

        [Test]
        public void When_ReversingToOpenStatus_Should_ClearInvoiceDate()
        {
            var order = new Order();
            order.AddItem(1L, 10m, 2m);

            order.InvoiceOrder();
            order.ReverseToOpen();

            order.InvoiceDate.ShouldBeNull();
        }

        [Test]
        public void When_AddingItemsWithDecimalValues_Should_CalculateTotalItemsValueCorrectly()
        {
            var order = new Order();

            order.AddItem(1L, 10.33m, 2.5m);  // Total: 25.825
            order.AddItem(2L, 7.99m, 3.75m);  // Total: 29.9625
            order.AddItem(3L, 15.50m, 1.25m); // Total: 19.375

            order.TotalItemsValue.ShouldBe(75.1625m);
        }
    }
}
