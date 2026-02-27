using Sales.Application.Contracts.Repositories;
using Sales.Application.Exceptions;
using Sales.Application.Helpers;
using Sales.Application.UseCases.CreateOrder.Validator;
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
        private readonly CreateOrderCommandValidator _validator;
        public CreateOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
            _validator = new();
        }

        public void Handle(CreateOrderCommand command)//todo: change return type when implement infrastructure
        {
            var result = _validator.Validate(command);

            if (!(result.IsValid))
            {
                throw new CreateOrderInvalidInputsException(result.Errors);
            }

            var order = new Order();

            foreach (var item in command.Items)
            {                

                if (item.DiscountValue.HasValue)
                {                    
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

                order.ApplyOrderDiscount(discount);
            }

            _repository.Insert(order);
        }
    }
}
