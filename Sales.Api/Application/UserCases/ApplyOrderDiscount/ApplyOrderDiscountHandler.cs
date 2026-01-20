using Sales.Api.Application.UserCases.Contracts;
using Sales.Api.Application.UserCases.Exceptions;

namespace Sales.Api.Application.UserCases.ApplyOrderDiscount
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
            var order = _repository.GetOrderById(command.OrderId) ?? throw new OrderNotFoundException(command.OrderId);
        }
    }
}
