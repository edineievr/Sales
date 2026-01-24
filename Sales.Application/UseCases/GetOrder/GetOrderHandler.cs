using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.GetOrder
{
    public class GetOrderHandler
    {
        private readonly IOrderRepository _repository;
        public GetOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public GetOrderResult Handle(GetOrderQuery query)
        {
            var order = _repository.GetById(query.OrderId) ?? throw new OrderNotFoundException(query.OrderId);

            var result = new GetOrderResult
            {
                id = order.Id,                
                TotalItemValue = order.TotalItemsValue,
                TotalOrderValue = order.TotalOrderValue,
                CreationDate = order.CreationDate,
                InvoiceDate = order.InvoiceDate,
                CancelationDate = order.CancelationDate,
                Status = order.Status.ToString(),
                Discount = order.Discount?.Value,
                DiscountType = order.Discount?.Type.ToString(),
                Items = order.Items.Select(item => new GetOrderItemResult
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalValue = item.TotalValue,
                    Id = item.Id,
                    Discount = item.Discount?.Value,
                    DiscountType = item.Discount?.Type.ToString()

                }).ToList(),
            };

            return result;

        }
    }
}
