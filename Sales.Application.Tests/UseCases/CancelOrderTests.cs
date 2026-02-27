using Sales.Application.Exceptions;
using Sales.Application.Tests.FakeRepositories;
using Sales.Application.UseCases.CancelOrder;
using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.Exceptions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using OrderNotFoundException = Sales.Application.Exceptions.OrderNotFoundException;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class CancelOrderTests
    {
        [Test]
        public void When_CancelingOrder_ShouldChangeStatusToCanceled()
        {
            var repository = new FakeOrderRepository();

            var order = new Order();
            order.AddItem(1, 100m, 1);

            order.TotalItemsValue.ShouldBe(100m);
            order.TotalOrderValue.ShouldBe(100m);

            repository.Insert(order);

            var handler = new CancelOrderHandler(repository);

            var command = new CancelOrderCommand
            {
                OrderId = order.Id
            };

            handler.Handle(command);

            var canceledOrder = repository.GetById(order.Id);

            canceledOrder.Status.ShouldBe(OrderStatus.Canceled);
            canceledOrder.CancelationDate.ShouldNotBeNull();
        }

        [Test]
        public void When_CancelingNonExistentOrder_Should_ThrowException()
        {
            var repository = new FakeOrderRepository();
            var handler = new CancelOrderHandler(repository);
            var command = new CancelOrderCommand
            {
                OrderId = 999 // Non-existent order ID
            };
            Should.Throw<OrderNotFoundException>(() =>
            {
                handler.Handle(command);
            });
        }
    }
}
