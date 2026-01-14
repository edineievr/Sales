using PedidoDeVenda.Entities.Exceptions;

namespace PedidoDeVenda.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; private set; }

        public ItemPedido(Produto produto, int quantidade, decimal precoUnitario)
        {
            if (produto == null)
            {
                throw new DomainException("Produto não pode ser nulo.");
            }

            if (quantidade <= 0)
            {
                throw new DomainException("Quantidade deve ser maior que 0.");
            }

            if (precoUnitario <= 0)
            {
                throw new DomainException("Valor nao pode ser menor que 0.");
            }
            
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public decimal TotalItem()
        {         
            decimal total = Quantidade * PrecoUnitario;

            return total;
        }
    }
}
