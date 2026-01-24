using Sales.Application.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.ListOrders
{
    public class ListOrdersHandler
    {
        private readonly IOrderRepository _repository;

        public ListOrdersHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public ListOrdersResult Handle()
        {
            var orders = _repository.GetAll();

            var result = new ListOrdersResult
            {
                Orders = orders.Select(order => new OrderDto
                {
                    Id = order.Id,
                    CreationDate = order.CreationDate,
                    Status = order.Status.ToString(),
                    TotalOrderValue = order.TotalOrderValue

                }).ToList()
            };

            return result;
        }

    }
}
