using Sales.Application.Exceptions;
using Sales.Application.Tests.FakeRepositories;
using Sales.Application.UseCases.RemoveOrderDiscount;
using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;
using Shouldly;

namespace Sales.Application.Tests.UseCases
{
    [TestFixture]
    public class RemoveOrderDiscountTests
    {
        [Test]
        public void When_RemovingExistingOrderDiscount_Should_Succeed()
        {
            var repository = new FakeOrderRepository();

            var handler = new RemoveOrderDiscountHandler(repository);

            var order = new Order();
                        
            order.AddItem(1L, 50m, 5m);

            var discount = new Discount(10, DiscountType.Percentage);

            order.ApplyOrderDiscount(discount);            

            order.TotalItemsValue.ShouldBe(250m);
            order.TotalOrderValue.ShouldBe(225m); // 10% discount applied

            repository.InsertOrder(order);

            var command = new RemoveOrderDiscountCommand { OrderId = order.Id };

            // Act
            var result = handler.Handle(command);

            var updatedOrder = repository.GetById(order.Id);

            updatedOrder.ShouldNotBeNull();
            updatedOrder.Discount.ShouldBeNull();
            updatedOrder.TotalItemsValue.ShouldBe(250m);
            updatedOrder.TotalOrderValue.ShouldBe(250m); // No discount
        }

        [Test]
        public void When_RemovingDiscountFromNonExistentOrder_Should_ThrowException()
        {
            var repository = new FakeOrderRepository();
            var handler = new RemoveOrderDiscountHandler(repository);

            var command = new RemoveOrderDiscountCommand { OrderId = 999L }; // Non-existent order ID

            Should.Throw<OrderNotFoundException>(() =>
            {
                handler.Handle(command);
            });
        }
    }
}
