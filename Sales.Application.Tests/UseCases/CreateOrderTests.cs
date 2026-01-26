using Sales.Application.Exceptions;
using Sales.Application.Tests.FakeRepositories;
using Sales.Application.UseCases.CreateOrder;
using Sales.Application.UseCases.CreateOrder.Validator;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class CreateOrderTests
    {
        [Test]
        public void When_CreatingNewOrder_Should_Succeed()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();

            var item1 = new CreateOrderItemDto
            {
                ProductId = 1,
                UnitPrice = 50m,
                Quantity = 2
            };

            var item2 = new CreateOrderItemDto
            {
                ProductId = 2,
                UnitPrice = 30m,
                Quantity = 1
            };
            command.Items.Add(item1);
            command.Items.Add(item2);

            // Act
            handler.Handle(command);

            // Assert
            var createdOrder = repository.GetById(0);

            createdOrder.ShouldNotBeNull();
            createdOrder.Items.Count.ShouldBe(2);
            createdOrder.Status.ShouldBe(OrderStatus.Open);
            createdOrder.TotalItemsValue.ShouldBe(130m);
            createdOrder.TotalOrderValue.ShouldBe(130m);
        }

        [Test]
        public void When_CreatingOrderWithNoItems_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();

            // Act & Assert
            Should.Throw<CreateOrderInvalidInputsException>(() => handler.Handle(command));
        }

        [Test]
        public void When_CreatingOrderWithInvalidItem_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();
            var invalidItem = new CreateOrderItemDto
            {
                ProductId = 0, // Invalid ProductId
                UnitPrice = -10m, // Invalid UnitPrice
                Quantity = 1
            };
            command.Items.Add(invalidItem);

            // Act & Assert
            Should.Throw<CreateOrderInvalidInputsException>(() => handler.Handle(command));
        }

        [Test]
        public void When_CreatingOrderWithInvalidDiscount_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();
            var item = new CreateOrderItemDto
            {
                ProductId = 1,
                UnitPrice = 50m,
                Quantity = 2,
                DiscountValue = -5m, // Invalid discount value
                DiscountType = "Percentage"
            };

            command.Items.Add(item);

            // Act & Assert
            Should.Throw<CreateOrderInvalidInputsException>(() => handler.Handle(command));
        }

        [Test]
        public void When_CreatingOrderWithInvalidDiscountType_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();
            var item = new CreateOrderItemDto
            {
                ProductId = 1,
                UnitPrice = 50m,
                Quantity = 2,
                DiscountValue = 10m,
                DiscountType = "" // Invalid discount type
            };
            command.Items.Add(item);

            // Act & Assert
            Should.Throw<CreateOrderInvalidInputsException>(() => handler.Handle(command));
        }

        [Test]
        public void When_CreatingOrderWithDiscount_Should_Succeed()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();
            var item1 = new CreateOrderItemDto
            {
                ProductId = 1,
                UnitPrice = 100m,
                Quantity = 1
            };
            command.Items.Add(item1);
            command.DiscountValue = 10m;
            command.DiscountType = "FixedAmount";
            // Act
            handler.Handle(command);
            // Assert
            var createdOrder = repository.GetById(0);
            createdOrder.ShouldNotBeNull();
            createdOrder.Items.Count.ShouldBe(1);
            createdOrder.Status.ShouldBe(OrderStatus.Open);
            createdOrder.TotalItemsValue.ShouldBe(100m);
            createdOrder.TotalOrderValue.ShouldBe(90m); // 100 - 10 discount
        }

        [Test]
        public void When_CreatingOrderWithInvalidItemLevelDiscount_Should_ThrowException()
        {
            // Arrange
            var repository = new FakeOrderRepository();
            var handler = new CreateOrderHandler(repository);
            var command = new CreateOrderCommand();
            var item1 = new CreateOrderItemDto
            {
                ProductId = 1,
                UnitPrice = 100m,
                Quantity = 1,
                DiscountValue = -5m, // Invalid discount value
                DiscountType = "FixedAmount"
            };
            command.Items.Add(item1);
            // Act & Assert
            Should.Throw<CreateOrderInvalidInputsException>(() => handler.Handle(command));
        }
    }
}
