using Sales.Domain.Orders.Entities;
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
    public class OrderItemDiscountTests
    {
        [Test]
        public void When_ApplyingDiscount_Should_AssociateDiscountToOrderItem()
        {
            var productId = 1L;
            var quantity = 5m;
            var unitPrice = 20m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            var discount = new Discount(10m, DiscountType.Percentage);
            orderItem.ApplyDiscount(discount);

            orderItem.Discount.ShouldNotBeNull();
            orderItem.Discount.Value.ShouldBe(10m);
            orderItem.Discount.Type.ShouldBe(DiscountType.Percentage);
        }

        [Test]
        public void When_CalculatingTotalPrice_WithPercentageDiscount_Should_ApplyDiscountOverTotalPrice()
        {
            var productId = 1L;
            var quantity = 10m;
            var unitPrice = 20m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            var discount = new Discount(10m, DiscountType.Percentage);// 10% discount
            orderItem.ApplyDiscount(discount);

            orderItem.CalculateGrossPrice().ShouldBe(200m);
            orderItem.CalculateTotalPrice().ShouldBe(180m);
        }

        [Test]
        public void When_CalculatingTotalPrice_WithFixedAmountDiscount_Should_ApplyDiscountOverTotalPrice()
        {
            var productId = 1L;
            var quantity = 10m;
            var unitPrice = 20m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            var discount = new Discount(15m, DiscountType.FixedAmount);// flat discount: 15
            orderItem.ApplyDiscount(discount);

            orderItem.CalculateGrossPrice().ShouldBe(200m);
            orderItem.CalculateTotalPrice().ShouldBe(185m);
        }

        [Test]
        public void When_CalculatingTotalPrice_WithDiscountExceedingGrossPrice_Should_ThrowException()
        {
            var productId = 1L;
            var quantity = 2m;
            var unitPrice = 10m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            var discount = new Discount(25m, DiscountType.FixedAmount);// flat discount: 25
            orderItem.ApplyDiscount(discount);

            Should.Throw<DiscountExceedsOrderItemValueException>(() =>
            {
                orderItem.CalculateTotalPrice();
            });
        }

        [Test]
        public void When_CalculatingTotalPrice_WithoutDiscount_Should_ReturnGrossPrice()
        {
            var productId = 1L;
            var quantity = 3m;
            var unitPrice = 15m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);

            orderItem.CalculateGrossPrice().ShouldBe(45m);
            orderItem.CalculateTotalPrice().ShouldBe(45m);
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
    }
}
