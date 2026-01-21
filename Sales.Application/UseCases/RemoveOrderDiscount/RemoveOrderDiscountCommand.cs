using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.UseCases.RemoveOrderDiscount
{
    public class RemoveOrderDiscountCommand
    {
        public long OrderId { get; set; }
    }
}
