namespace Pedido.Domain.Product.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public int Saldo { get; private set; }
        public decimal Preco { get; private set; }

    }
}
