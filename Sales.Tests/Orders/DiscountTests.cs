using Sales.Domain.Orders.Entities.Enums;
using Sales.Domain.Orders.Exceptions;
using Sales.Domain.Orders.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class DiscountTests
    {

        [Test]
        public void When_CreatingDiscount_Should_Succeed()
        {
            var validDiscountValue = 15m;
            var discount = new Discount(validDiscountValue, DiscountType.Percentage);

            discount.Value.ShouldBe(validDiscountValue);
            discount.Type.ShouldBe(DiscountType.Percentage);
        }

        [Test]
        public void When_CreatingDiscountWithInvalidValue_Should_ThrowException()
        {
            var invalidDiscountValue = 0m;

            Should.Throw<InvalidDiscountValueException>(() =>
            {
                var discount = new Discount(invalidDiscountValue, DiscountType.Percentage);
            });
        }

        [Test]
        public void When_CreatingDiscountWithNegativeValue_Should_ThrowException()
        {
            var negativeDiscountValue = -5m;

            Should.Throw<InvalidDiscountValueException>(() =>
            {
                var discount = new Discount(negativeDiscountValue, DiscountType.FixedAmount);
            });
        }

        [Test]
        public void When_ApplyingPercentageDiscount_Should_CalculateCorrectly()
        {
            var discountValue = 10m; // 10%

            var discount = new Discount(discountValue, DiscountType.Percentage);
            var amount = 200m;
            var discountedAmount = discount.ApplyDiscount(amount);

            discountedAmount.ShouldBe(180m); // 200 - (10% of 200)
        }

        [Test]
        public void When_ApplyingFixedAmountDiscount_Should_CalculateCorrectly()
        {
            var discountValue = 30m;// flat discount: 30
            var discount = new Discount(discountValue, DiscountType.FixedAmount);
            var amount = 200m;

            var discountedAmount = discount.ApplyDiscount(amount);

            discountedAmount.ShouldBe(170m);
        }
    }
}
