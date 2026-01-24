using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;

namespace Sales.Application.UseCases.ApplyOrderDiscount
{
    public class ApplyOrderDiscountHandler
    {
        private readonly IOrderRepository _repository;

        public ApplyOrderDiscountHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public ApplyOrderDiscountResult Handle(ApplyOrderDiscountCommand command)
        {
            var order = _repository.GetById(command.OrderId) ?? throw new OrderNotFoundException(command.OrderId);

            if (!Enum.TryParse<DiscountType>(command.DiscountType, ignoreCase: true, out var discountType))
            {
                throw new InvalidDiscountTypeException(command.DiscountType);
            }

            var discount = new Discount(command.DiscountValue, discountType);

            order.SetDiscount(discount);

            _repository.UpdateOrder(order);

            return new ApplyOrderDiscountResult { OrderId = order.Id };
        }
    }
}

