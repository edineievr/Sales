namespace Sales.Application.UseCases.ApplyOrderDiscount
{
    public class ApplyOrderDiscountCommand
    {
        public long OrderId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; } = string.Empty;
    }
}
