using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;

namespace Sales.Application.UseCases.RemoveOrderDiscount
{
    public class RemoveOrderDiscountHandler
    {
        private readonly IOrderRepository _repository;
        public RemoveOrderDiscountHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public RemoveOrderDiscountResult Handle(RemoveOrderDiscountCommand command)
        {
            var order = _repository.GetOrderById(command.OrderId) ?? throw new OrderNotFoundException(command.OrderId);

            order.RemoveDiscount();

            _repository.UpdateOrder(order);

            return new RemoveOrderDiscountResult { OrderId = order.Id };
        }
    }
}
