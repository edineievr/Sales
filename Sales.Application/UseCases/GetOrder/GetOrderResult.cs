using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Sales.Application.UseCases.GetOrder
{
    public class GetOrderResult
    {
        public long id { get; set; }
        public List<GetOrderItemResult> Items { get; set; }
        public decimal TotalOrderValue { get; set; }
        public decimal TotalItemValue { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? CancelationDate { get; set; }
        public string Status { get; set; }
        public decimal? Discount { get; set; }
        public string? DiscountType { get; set; }



        public GetOrderResult()
        {
            Items = [];
            Discount = null;
            DiscountType = null;
            InvoiceDate = null;
            CancelationDate = null;
            Status = string.Empty;
        }
    }
}
