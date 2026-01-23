using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.CreateOrder
{
    public class CreateOrderItemDto
    {
        public long ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal? DiscountValue { get; set; }
        public string? DiscountType { get; set; }

        public CreateOrderItemDto()
        {
            DiscountType = null;
            DiscountValue = null;
        }
    }
}
