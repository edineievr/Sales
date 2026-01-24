using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.ListOrders
{
    public class OrderDto
    {
        public long Id { get; set; }
        public decimal TotalOrderValue { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }

        public OrderDto()
        {
            Status = string.Empty;
        }
    }
}
