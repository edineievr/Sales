using System.ComponentModel;

namespace Sales.Domain.Orders.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("Open order")]
        Open = 0,
        [Description("Invoiced order")]
        Invoiced = 1,
        [Description("Cancelled order")]
        Cancelled = 2
    }
}
