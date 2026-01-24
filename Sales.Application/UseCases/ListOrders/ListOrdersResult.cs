using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Sales.Application.UseCases.ListOrders
{
    public class ListOrdersResult
    {
        public List<OrderDto> Orders { get; set; }

        public ListOrdersResult()
        {
            Orders = [];
        }
    }
}
