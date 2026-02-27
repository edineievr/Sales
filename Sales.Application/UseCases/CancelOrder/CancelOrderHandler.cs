using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CancelOrder
{
    public class CancelOrderHandler
    {
        private readonly IOrderRepository _repository;
        public CancelOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CancelOrderCommand command)
        {
            var order = _repository.GetById(command.OrderId) ?? throw new OrderNotFoundException(command.OrderId);

            order.CancelOrder();

            _repository.Update(order);
        }
    }
}
