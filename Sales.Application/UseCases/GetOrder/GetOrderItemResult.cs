using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.GetOrder
{
    public class GetOrderItemResult
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public decimal? Discount { get; set; }
        public string? DiscountType { get; set; }

        public GetOrderItemResult()
        {
            Discount = null;
            DiscountType = null;
        }
    }
}
