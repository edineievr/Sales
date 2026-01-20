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

            order.SetDiscount(discount);

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
            order.SetDiscount(discount);

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
            order.SetDiscount(discount);

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
            order.SetDiscount(discount);

            Should.Throw<DiscountExceedsOrderValueException>(() => 
            {
                var total = order.TotalOrderValue;
            });
        }
    }
}
