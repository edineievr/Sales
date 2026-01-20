using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Exceptions;
using Shouldly;

namespace Sales.Tests.Unit.Orders
{
    [TestFixture]
    public class OrderItemEntityTests
    {
        [Test]
        public void When_NewOrderItemIsCreatedWithInvalidUnitPrice_Should_ThrowException()
        {
            var productId = 1L;
            var invalidUnitPrice = 0m;
            var quantity = 5m;
            
            Should.Throw<InvalidOrderItemUnitPriceException>(() =>
            {
                var orderItem = new OrderItem(productId, invalidUnitPrice, quantity);
            });            
        }

        [Test]
        public void When_NewOrderItemIsCreatedWithInvalidQuantity_Should_ThrowException()
        {
            var productId = 1L;
            var unitPrice = 10m;
            var invalidQuantity = 0m;

            Should.Throw<InvalidOrderItemQuantityException>(() =>
            {
                var orderItem = new OrderItem(productId, unitPrice, invalidQuantity);
            });
        }

        [Test]
        public void When_NewOrderItemIsCreatedWithInvalidProductId_Should_ThrowException()
        {
            var invalidProductId = 0L;
            var unitPrice = 10m;
            var quantity = 5m;

            Should.Throw<InvalidOrderItemProductIdException>(() =>
            {
                var orderItem = new OrderItem(invalidProductId, unitPrice, quantity);
            });
        }
    }
}
