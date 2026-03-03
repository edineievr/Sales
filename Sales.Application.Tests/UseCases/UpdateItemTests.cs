using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class UpdateItemTests
    {
        [Test]
        public void When_UpdateItemWithDiscount_Should_UpdateItem()
        {
            var order= new Order();

            var discount = new Discount(10, DiscountType.Percentage);

            order.AddItem(1, 10, 2, discount);
            order.AddItem(2, 20, 1, null);

            order.TotalOrderValue.ShouldBe(38); // 10% discount

            order.UpdateItem(0, 1, 15, 3, discount);

            order.TotalOrderValue.ShouldBe(60.5m); // 10% discount
        }

        [Test]
        public void When_UpdateItemWithoutDiscount_Should_UpdateItem()
        {
            var order = new Order();

            order.AddItem(1, 10, 2, null);
            order.AddItem(2, 20, 1, null);
            
            order.TotalOrderValue.ShouldBe(40);

            order.UpdateItem(0, 1, 15, 3, null);

            order.TotalOrderValue.ShouldBe(65);
        }
    }
}
