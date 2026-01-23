using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;
using Sales.Application.Helpers;
using Sales.Domain.Orders.Entities;
using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly IOrderRepository _repository;
        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateOrderCommand command)//todo: change return type when implement infrastructure
        {
            var order = new Order();

            foreach (var item in command.Items)
            {                

                if (item.DiscountValue.HasValue)
                {
                    if (string.IsNullOrWhiteSpace(item.DiscountType))
                    {
                        throw new InvalidDiscountTypeException(item.DiscountType);
                    }
                    var discount = DiscountParser.Parse(item.DiscountValue.Value, item.DiscountType);                    

                    order.AddItem(item.ProductId, item.UnitPrice, item.Quantity, discount);
                }
                else
                {
                    order.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
                }
            }

            if (command.DiscountValue.HasValue)
            {
                if (string.IsNullOrWhiteSpace(command.DiscountType))
                {
                    throw new InvalidDiscountTypeException(command.DiscountType);
                }                   

                var discount = DiscountParser.Parse(command.DiscountValue.Value, command.DiscountType);

                order.SetDiscount(discount);
            }

            _repository.InsertOrder(order);
        }
    }
}
