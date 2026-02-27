using Sales.Application.Exceptions;
using Sales.Application.Tests.FakeRepositories;
using Sales.Application.UseCases.ApplyOrderDiscount;
using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class ApplyOrderDiscountTests
    {
        [Test]
        public void When_ApplyingValidOrderDiscount_Should_UpdateOrder()
        {
            // Arrange
            var repository = new FakeOrderRepository();

            var order = new Order();
            order.AddItem(1, 100m, 1);

            order.TotalItemsValue.ShouldBe(100m);
            order.TotalOrderValue.ShouldBe(100m);

            repository.Insert(order);

            var handler = new ApplyOrderDiscountHandler(repository);

            var command = new ApplyOrderDiscountCommand
            {
                OrderId = order.Id,
                DiscountValue = 10m,
                DiscountType = "Percentage"
            };

            // Act
            handler.Handle(command);

            // Assert
            var updatedOrder = repository.GetById(order.Id);

            updatedOrder.ShouldNotBeNull();
            updatedOrder.Discount.ShouldNotBeNull();
            updatedOrder.Discount.Value.ShouldBe(10m);
            updatedOrder.Discount.Type.ShouldBe(DiscountType.Percentage);
            updatedOrder.TotalItemsValue.ShouldBe(100m);
            updatedOrder.TotalOrderValue.ShouldBe(90m);
        }

        [Test]
        public void When_ApplyingDiscountToNonExistentOrder_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();

            var handler = new ApplyOrderDiscountHandler(repository);

            var command = new ApplyOrderDiscountCommand
            {
                OrderId = 999, // Non-existent order ID
                DiscountValue = 10m,
                DiscountType = "Percentage"
            };
            // Act & Assert
            Should.Throw<OrderNotFoundException>(() =>
            {
                handler.Handle(command).OrderId.ShouldBe(999);
            });
        }

        [Test]
        public void When_ApplyingDiscountWithInvalidType_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();

            var order = new Order();
            order.AddItem(1, 100m, 1);

            repository.Insert(order);

            var handler = new ApplyOrderDiscountHandler(repository);

            var command = new ApplyOrderDiscountCommand
            {
                OrderId = order.Id,
                DiscountValue = 10m,
                DiscountType = "InvalidType"
            };
            // Act & Assert
            Should.Throw<InvalidDiscountTypeException>(() =>
            {
                handler.Handle(command);
            });
        }
    }
}
