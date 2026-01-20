using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
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
            orderItem.CalculateTotalValue().ShouldBe(180m);
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
            orderItem.CalculateTotalValue().ShouldBe(185m);
        }

        [Test]
        public void When_CalculatingTotalValue_WithDiscountExceedingGrossPrice_Should_ThrowException()
        {
            var productId = 1L;
            var quantity = 2m;
            var unitPrice = 10m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);
            var discount = new Discount(25m, DiscountType.FixedAmount);// flat discount: 25
            orderItem.ApplyDiscount(discount);

            Should.Throw<DiscountExceedsOrderItemValueException>(() =>
            {
                orderItem.CalculateTotalValue();
            });
        }

        [Test]
        public void When_CalculatingTotalValue_WithoutDiscount_Should_ReturnGrossPrice()
        {
            var productId = 1L;
            var quantity = 3m;
            var unitPrice = 15m;

            var orderItem = new OrderItem(productId, quantity, unitPrice);

            orderItem.CalculateGrossPrice().ShouldBe(45m);
            orderItem.CalculateTotalValue().ShouldBe(45m);
        }        
    }
}
