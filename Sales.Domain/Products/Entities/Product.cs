namespace Sales.Domain.Products.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public int Stock { get; private set; }
        public decimal Price { get; private set; }

        public Product()
        {
            Description = string.Empty;
            Stock = 0;
            Price = 0;
        }

    }
}
