namespace Sales.Application.UseCases.CreateOrder
{
    public class CreateOrderCommand
    {
        public List<CreateOrderItemDto> Items { get; set; }
        public decimal? DiscountValue { get; set; }
        public string? DiscountType { get; set; }

        public CreateOrderCommand()
        {
            Items = [];
            DiscountType = null;
            DiscountValue = null;
        }
    }
}
