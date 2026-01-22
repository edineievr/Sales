using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CreateOrder
{
    public class CreateOrderCommand
    {
        public List<OrderItemDto> Items { get; set; }
        public decimal? DiscountValue { get; set; }
        public string? DiscountType { get; set; }

        public CreateOrderCommand()
        {
            Items = [];
            DiscountType = null;
            DiscountValue = null;
        }
    }
}
