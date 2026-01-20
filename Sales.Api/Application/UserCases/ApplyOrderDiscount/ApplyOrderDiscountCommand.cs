using Sales.Domain.Orders.Enums;
using Sales.Domain.Orders.ValueObjects;

namespace Sales.Api.Application.UserCases.ApplyOrderDiscount
{
    public class ApplyOrderDiscountCommand
    {
        public long OrderId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; } = string.Empty;
    }
}
