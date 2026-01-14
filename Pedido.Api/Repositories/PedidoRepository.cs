using PedidoDeVenda.Entities;
using PedidoDeVenda.Entities.Exceptions;
using PedidoDeVenda.Repositories.Interfaces;

namespace PedidoDeVenda.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly List<Pedido> _pedidos;

        public PedidoRepository() {

            _pedidos = new List<Pedido>();
        }

        public void CriarPedido(Pedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public void RemoverPedido(int id)
        {
            var p = BuscaPorId(id);

            _pedidos.Remove(p);

        }

        public List<Pedido> ListarTodos()
        {
            return _pedidos;
        }

        public Pedido BuscaPorId(int id)
        {
            var pedido = _pedidos.FirstOrDefault(x => x.Id == id);
            
            return pedido;
        }

        public void AdicionarItem(int idPedido, ItemPedido itemPedido)
        {
            var p = BuscaPorId(idPedido);

            p.AdicionaItem(itemPedido);             

        }

        public void RemoverItem(int idPedido, int id)
        {
            var p = BuscaPorId(idPedido);

            p.RemoveItem(id);                       
        }
    }
}
