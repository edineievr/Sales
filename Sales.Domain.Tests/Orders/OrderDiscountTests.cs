using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;
using Sales.Domain.Orders.ValueObjects;
using Shouldly;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class OrderDiscountTests
    {
        [Test]
        public void When_ApplyingDiscount_Should_AssociateDiscountToOrder()
        {
            var order = new Order();
            var discount = new Discount(20m, DiscountType.FixedAmount);

            order.ApplyOrderDiscount(discount);

            order.Discount.ShouldNotBeNull();
            order.Discount.Value.ShouldBe(20m);
            order.Discount.Type.ShouldBe(DiscountType.FixedAmount);
        }

        [Test]
        public void When_CalculatingTotalOrderValue_WithFlatOrderDiscount_Should_ApplyDiscountOverOrderTotal()
        {
            var order = new Order();
            order.AddItem(1L, 50m, 2m); // Total: 100
            order.AddItem(2L, 30m, 1m); // Total: 30

            var discount = new Discount(15m, DiscountType.FixedAmount); // Flat discount: 15
            order.ApplyOrderDiscount(discount);

            order.TotalItemsValue.ShouldBe(130m);
            order.TotalOrderValue.ShouldBe(115m);
        }

        [Test]
        public void When_CalculatingTotalOrderValue_WithPercentageOrderDiscount_Should_ApplyDiscountOverOrderTotal()
        {
            var order = new Order();
            order.AddItem(1L, 100m, 1m); // Total: 100
            order.AddItem(2L, 50m, 2m);  // Total: 100

            var discount = new Discount(10m, DiscountType.Percentage); // 10% discount
            order.ApplyOrderDiscount(discount);

            order.TotalItemsValue.ShouldBe(200m);
            order.TotalOrderValue.ShouldBe(180m);
        }

        [Test]
        public void When_CalculatingTotalOrderValue_WithDiscountExceedingTotal_Should_ThrowException()
        {
            var order = new Order();
            order.AddItem(1L, 100m, 1m); // Total: 100
            order.AddItem(2L, 50m, 2m);  // Total: 100

            var discount = new Discount(210m, DiscountType.FixedAmount); // Flat discount: 210
            order.ApplyOrderDiscount(discount);

            Should.Throw<DiscountExceedsAmountException>(() =>
            {
                var total = order.TotalOrderValue;
            });
        }

        [Test]
        public void When_RemovingDiscount_Should_RemoveDiscountFromOrder()
        {
            var order = new Order();

            var discount = new Discount(20m, DiscountType.FixedAmount);
            order.ApplyOrderDiscount(discount);

            order.RemoveDiscount();

            order.Discount.ShouldBeNull();
        }

        [Test]
        public void When_RemovingDiscount_FromNonEditableOrder_Should_ThrowException()
        {
            var order = new Order();

            order.AddItem(1L, 100m, 1m);

            var discount = new Discount(20m, DiscountType.FixedAmount);

            order.ApplyOrderDiscount(discount);
            order.InvoiceOrder();

            Should.Throw<OrderIsNotEditableException>(() =>
            {
                order.RemoveDiscount();
            });
        }

        [Test]
        public void When_ApplyingOrderDiscount_WithItemLevelDiscount_Should_ThrowException()
        {
            var order = new Order();
            order.AddItem(1L, 100m, 1m);
            order.AddItem(2L, 50m, 2m);

            var itemDiscount = new Discount(10m, DiscountType.Percentage);
            order.ApplyItemDiscount(0, itemDiscount);

            var orderDiscount = new Discount(20m, DiscountType.FixedAmount);

            Should.Throw<OrderDiscountConflictException>(() =>
            {
                order.ApplyOrderDiscount(orderDiscount);
            });
        }

        [Test]
        public void When_ApplyingItemLevelDiscount_WithOrderLevelDiscount_Should_ThrowException()
        {
            var order = new Order();
            order.AddItem(1L, 100m, 1m);
            order.AddItem(2L, 50m, 2m);
            var orderDiscount = new Discount(20m, DiscountType.FixedAmount);
            order.ApplyOrderDiscount(orderDiscount);
            var itemDiscount = new Discount(10m, DiscountType.Percentage);

            Should.Throw<OrderDiscountConflictException>(() =>
            {
                order.ApplyItemDiscount(0, itemDiscount);
            });
        }
    }
}
