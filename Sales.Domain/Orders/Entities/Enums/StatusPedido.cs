using System.ComponentModel;

namespace Order.Domain.Orders.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("Pending order")]
        Pending = 0,
        [Description("Open order")]
        Open = 1,
        [Description("Cancelled order")]
        Cancelled = 2
    }
}
