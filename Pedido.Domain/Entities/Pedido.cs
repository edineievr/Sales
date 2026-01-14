using PedidoDeVenda.Entities.Exceptions;
using PedidoDeVenda.Enums;

namespace PedidoDeVenda.Entities
{
    public class Pedido
    {

        public int Id { get; private set; }
        public List<ItemPedido> Itens { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public decimal ValorTotalItens { get; private set; }
        public StatusPedido Status { get; private set; }

        public Pedido(int id)
        {
            Id = id;
            Itens = new List<ItemPedido>();
            DataCriacao = DateTime.Now;
            Status = StatusPedido.Pendente;
        }

        public List<ItemPedido> ListarItens()
        {
            return Itens;
        }

        public ItemPedido BuscaPorId(int id)
        {
            var item = Itens.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                throw new DomainException("Item não encontrado.");
            }

            return item;
        }

        public void AdicionaItem(ItemPedido itemPedido)
        {          
            
            if (itemPedido == null)
            {
                throw new DomainException("Item não pode ser nulo.");
            }

            Itens.Add(itemPedido);
        }

        public void RemoveItem(int id)
        {
            var item = BuscaPorId(id);

            Itens.Remove(item);
        }

        public decimal CalculaTotalPedido()
        {
            decimal total = 0;

            foreach (ItemPedido item in Itens)
            {
                total += item.TotalItem();
            }

            return total;
        }

        public void GravaPedidoComItens()
        {
            if (Itens.Count == 0)
            {
                throw new DomainException("Não é possível gravar um pedido sem itens.");
            }

            Status = StatusPedido.Aberto;
        }

        public void CancelaPedido()
        {
            Status = StatusPedido.Cancelado;
        }

        public void FinalizaPedido()
        {
            if (Itens.Count == 0)
            {
                throw new DomainException("Não é possível finalizar um pedido sem itens.");
            }

            Status = StatusPedido.Finalizado;
        }
    }
}
